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

            updateSchedule(sys);

            return sys;
        }

        // Fonction de mise à jour des listes temporelles (Today, Tomorrow, ...) de l'échéancier
        public void updateSchedule(GTDSystem sys)
        {
            TaskList dueTodayTemp;
            // Ajout dans la liste "Today" des tâches de la Inbox dont la date de rendu
            // est aujourd'hui
            dueTodayTemp = new DynamicList(sys.Inbox, Algorithms.getDueToday);
            foreach (Task t in dueTodayTemp)
            {
                sys.Today.AddTask(t);
            }

            // Idem pour les tâches présentes dans les liste des contextes existants
            foreach (TaskList taskList in sys.Contexts)
            {
                dueTodayTemp = new DynamicList(taskList, Algorithms.getDueToday);
                foreach (Task t in dueTodayTemp)
                {
                    sys.Today.AddTask(t);
                }
            }
        }
    }
}
