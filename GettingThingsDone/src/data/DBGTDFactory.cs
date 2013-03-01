using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using GettingThingsDone.src.model;

namespace GettingThingsDone.src.data
{
    class DBGTDFactory : IGTDFactory
    {
        private IDatabaseProvider dbProvider;

        public DBGTDFactory(IDatabaseProvider dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public ISingleTask makeTask(string title, string description, DateTimeOffset? DueDate)
        {
            DataClassesDataContext dc = dbProvider.Database;

            Tasks dbTask = new Tasks();
            dbTask.CreationDate = DateTimeOffset.Now;
            dbTask.Title = title;
            dbTask.Description = description;
            dbTask.DueDate = DueDate;
            dbTask.Owner = (App.Current as App).Admin.Id;

            dc.Tasks.InsertOnSubmit(dbTask);
            dc.SubmitChanges();

            return new DBSingleTask(dbTask, this.dbProvider);
        }

        public IProject makeProject(string title)
        {
            throw new NotImplementedException();
        }

        public GTDSystem makeSystem()
        {
            GTDSystem sys = new GTDSystem();

            DataClassesDataContext db = dbProvider.Database;
            foreach (Tasks t in db.Tasks)
            {
                ISingleTask st = new DBSingleTask(t, dbProvider);
                sys.Inbox.List.Add(st);
            }

            //updateSchedule(sys);

            return sys;
        }

        // Fonction de mise à jour des listes temporelles (Today, Tomorrow, ...) de l'échéancier
        public void updateSchedule(GTDSystem sys)
        {
            System.Console.WriteLine("ceylemal");
            TaskList due;
            Func<GTDItem, Boolean> filter = null;

            foreach (TaskList schedule in sys.Schedules) 
            {
                // Choix du filtre à utiliser selon la liste
                if (schedule.Name == "Today")
                    filter = Algorithms.getDueToday;
                else if (schedule.Name == "Tomorrow")
                    filter = Algorithms.getDueTomorrow;
                else if (schedule.Name == "Next week")
                    filter = Algorithms.getDueNextWeek;
                else if (schedule.Name == "Next Month")
                    filter = Algorithms.getDueNextMonth;
                else
                    filter = null;

                if (filter != null)
                {
                    ((StaticList)schedule).List.Clear();

                    // Ajout des tâches venant de la inbox
                    // dans la liste actuelle
                    due = new DynamicList(sys.Inbox, filter);
                    foreach (Task t in due)
                    {
                        schedule.AddTask(t);
                    }

                    // Ajout des tâches venant de chaque contexte
                    foreach (TaskList taskList in sys.Contexts)
                    {
                        due = new DynamicList(taskList, filter);
                        foreach (Task t in due)
                        {
                            schedule.AddTask(t);
                        }
                    }
                }
            }
        }
    }
}
