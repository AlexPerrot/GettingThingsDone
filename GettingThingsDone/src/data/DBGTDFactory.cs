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

        public ISingleTask makeTask(string title, string description, DateTimeOffset? DueDate, IUser owner)
        {
            DataClassesDataContext dc = dbProvider.Database;

            Tasks dbTask = new Tasks();
            dbTask.CreationDate = DateTimeOffset.Now;
            dbTask.Title = title;
            dbTask.Description = description;
            dbTask.DueDate = DueDate;
            dbTask.Owner = dbProvider.IdManager.GetId(owner);

            dc.Tasks.InsertOnSubmit(dbTask);
            dc.SubmitChanges();

            return new DBSingleTask(dbTask, this.dbProvider);
        }

        public IProject makeProject(string title, string description, DateTimeOffset? DueDate, IUser owner)
        {
            DataClassesDataContext dc = dbProvider.Database;

            Projects dbProject = new Projects();
            dbProject.CreationDate = DateTimeOffset.Now;
            dbProject.Title = title;
            dbProject.Description = description;
            dbProject.DueDate = DueDate;
            dbProject.Owner = dbProvider.IdManager.GetId(owner);
            
            dc.Projects.InsertOnSubmit(dbProject);
            dc.SubmitChanges();

            return new DBProject(dbProject, this.dbProvider);
        }

        public IStaticList makeContext(string title, string description, IUser owner)
        {
            DataClassesDataContext dc = dbProvider.Database;
            Lists dbList = new Lists();
            dbList.Title = title;
            dbList.Description = description;
            dbList.Owner = dbProvider.IdManager.GetId(owner);

            dc.Lists.InsertOnSubmit(dbList);
            dc.SubmitChanges();

            return new DBStaticList(dbList, dbProvider);
        }

        public IGTDSystem makeSystem(IUser owner)
        {
            GTDSystem sys = new GTDSystem(owner);
            IDictionary<int, ISingleTask> taskMap = new Dictionary<int, ISingleTask>();
            IDictionary<int, IProject> projectMap = new Dictionary<int, IProject>();
            //IDictionary<int, IStaticList> listMap = new Dictionary<int, IStaticList>();

            DataClassesDataContext db = dbProvider.Database;

            int usrid = dbProvider.IdManager.GetId(owner);

            foreach (Tasks t in db.Tasks.Where(item => item.Owner == usrid))
            {
                ISingleTask st = new DBSingleTask(t, dbProvider);
                taskMap.Add(t.Id, st);
            }

            IDictionary<string, IStaticList> defaultListsMap = new Dictionary<string, IStaticList>();
            string[] names = new string[] {"Inbox", "Waiting", "Someday"};
            foreach (var name in names)
            {
                IEnumerable<Lists> listResults = db.Lists.Where(list => list.Title == name && list.Owner == usrid);
                if (listResults.Count() == 0)
                {
                    // si la requete est vide, il n'y a pas de liste avec ce nom, donc aucune tache dedans
                    defaultListsMap.Add(name, makeContext(name, "", owner));
                }
                else { defaultListsMap.Add(name, createDBStaticList(listResults.First(), taskMap)); } 
            }

            // Mise en place des tâches dans la Inbox
            sys.Inbox = defaultListsMap["Inbox"];
               
            // Mise en place des tâches dans Waiting
            sys.Waiting = defaultListsMap["Waiting"];

            // Mise en place des tâches dans Someday
            sys.Someday = defaultListsMap["Someday"];

            // Mise en place Contexts
            foreach (Lists list in db.Lists.Where(x => !names.Contains(x.Title)
                && x.Owner == usrid))
            {
                IStaticList stl = createDBStaticList(list, taskMap);
                sys.Contexts.Add(stl);
            }

            foreach (Projects project in db.Projects.Where(item => item.Owner == usrid))
            {
                IProject p = new DBProject(project, dbProvider);
                projectMap.Add(project.Id, p);
                sys.Projects.Add(p);
            }

            foreach (Projects_Tasks pt in db.Projects_Tasks.Where(item => item.Owner == usrid))
                projectMap[pt.Project_id].AddTask(taskMap[pt.Task_id]);

            return sys;
        }

        private IStaticList createDBStaticList(Lists list, IDictionary<int, ISingleTask> taskMap)
        {
            DataClassesDataContext db = dbProvider.Database;
            IStaticList stl = new DBStaticList(list, dbProvider);
            // attetion User codé en dur
            foreach (Lists_Tasks tl in db.Lists_Tasks.Where(x => x.List_id == list.Id && x.Owner == list.Owner))
            {
                stl.AddTask(taskMap[tl.Task_id]);
            }
            return stl;
        }

        public ISchedule makeSchedule(IGTDSystem source)
        {
            return new Schedule(source);
        }
    }
}
