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

            Lists_Tasks dbListTask = new Lists_Tasks();
            dbListTask.List_id = 1;
            dbListTask.Task_id = dbTask.Id;
            dbListTask.Owner = dbTask.Owner;

            dc.Lists_Tasks.InsertOnSubmit(dbListTask);
            dc.SubmitChanges();

            return new DBSingleTask(dbTask, this.dbProvider);
        }

        public IProject makeProject(string title, string description, DateTimeOffset? DueDate)
        {
            DataClassesDataContext dc = dbProvider.Database;

            Projects dbProject = new Projects();
            dbProject.CreationDate = DateTimeOffset.Now;
            dbProject.Title = title;
            dbProject.Description = description;
            dbProject.DueDate = DueDate;
            dbProject.Owner = (App.Current as App).Admin.Id;
            
            dc.Projects.InsertOnSubmit(dbProject);
            dc.SubmitChanges();

            return new DBProject(dbProject, this.dbProvider);
        }

        public IGTDSystem makeSystem()
        {
            GTDSystem sys = new GTDSystem();
            IDictionary<int, ISingleTask> taskMap = new Dictionary<int, ISingleTask>();
            IDictionary<int, IProject> projectMap = new Dictionary<int, IProject>();
            //IDictionary<int, IStaticList> listMap = new Dictionary<int, IStaticList>();

            DataClassesDataContext db = dbProvider.Database;
            
            // Mise en place des tâches dans la Inbox
            sys.Inbox = new DBStaticList("Inbox", 1, dbProvider);
            foreach (Lists_Tasks tl in db.Lists_Tasks.Where(x => x.List_id == 1 && x.Owner == 1))
            {
                Tasks t = db.Tasks.Single(x => x.Id == tl.Task_id);
                DBSingleTask st = new DBSingleTask(t, dbProvider);
                taskMap.Add(st.Id, st);
                sys.Inbox.AddTask(st);
            }

            // Mise en place des tâches dans Waiting
            sys.Waiting = new DBStaticList("Waiting", 2, dbProvider);
            foreach (Lists_Tasks tl in db.Lists_Tasks.Where(x => x.List_id == 2 && x.Owner == 1))
            {
                Tasks t = db.Tasks.Single(x => x.Id == tl.Task_id);
                DBSingleTask st = new DBSingleTask(t, dbProvider);
                taskMap.Add(st.Id, st);
                sys.Waiting.AddTask(st);
            }

            // Mise en place des tâches dans Someday
            sys.Someday = new DBStaticList("Someday", 3, dbProvider); 
            foreach (Lists_Tasks tl in db.Lists_Tasks.Where(x => x.List_id == 3 && x.Owner == 1))
            {
                Tasks t = db.Tasks.Single(x => x.Id == tl.Task_id);
                DBSingleTask st = new DBSingleTask(t, dbProvider);
                taskMap.Add(st.Id, st);
                sys.Someday.AddTask(st);
            }

            // Mise en place Contexts
            foreach (Lists list in db.Lists.Where(x => x.Id != 1 && x.Id != 2 && x.Id != 3 
                && x.Owner == 1))
            {
                IStaticList stl = new DBStaticList(list.Title, list.Id, dbProvider);
                
                foreach (Lists_Tasks tl in db.Lists_Tasks.Where(x => x.List_id == list.Id && x.Owner == 1))
                {
                    Tasks t = db.Tasks.Single(x => x.Id == tl.Task_id);
                    DBSingleTask st = new DBSingleTask(t, dbProvider);
                    taskMap.Add(st.Id, st);
                    stl.AddTask(st);
                }
                sys.Contexts.Add(stl);
            }

            foreach (Projects project in db.Projects)
            {
                IProject p = new DBProject(project, dbProvider);
                projectMap.Add(project.Id, p);
                sys.Projects.Add(p);
            }

            foreach (Projects_Tasks pt in db.Projects_Tasks)
                projectMap[pt.Project_id].AddTask(taskMap[pt.Task_id]);

            return sys;
        }

        public ISchedule makeSchedule(IGTDSystem source)
        {
            return new Schedule(source);
        }
    }
}
