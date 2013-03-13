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

            DataClassesDataContext db = dbProvider.Database;
            foreach (Tasks t in db.Tasks)
            {
                DBSingleTask st = new DBSingleTask(t, dbProvider);
                taskMap.Add(st.Id, st);
                sys.Inbox.AddTask(st);
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
