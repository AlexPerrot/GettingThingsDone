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

        public IStaticList makeContext(string title, string description)
        {
            DataClassesDataContext dc = dbProvider.Database;
            Lists dbList = new Lists();
            dbList.Title = title;
            dbList.Description = description;
            dbList.Owner = (App.Current as App).Admin.Id;

            dc.Lists.InsertOnSubmit(dbList);
            dc.SubmitChanges();

            return new DBStaticList(dbList.Title, dbList.Id, dbProvider);
        }

        public IGTDSystem makeSystem()
        {
            // TODO : utiliser une classe perso au passage au multi user
            Users user = (App.Current as App).Admin;

            GTDSystem sys = new GTDSystem();
            IDictionary<int, ISingleTask> taskMap = new Dictionary<int, ISingleTask>();
            IDictionary<int, IProject> projectMap = new Dictionary<int, IProject>();
            //IDictionary<int, IStaticList> listMap = new Dictionary<int, IStaticList>();

            DataClassesDataContext db = dbProvider.Database;

            foreach (Tasks t in db.Tasks)
            {
                ISingleTask st = new DBSingleTask(t, dbProvider);
                taskMap.Add(t.Id, st);
            }

            IDictionary<string, IStaticList> defaultListsMap = new Dictionary<string, IStaticList>();
            string[] names = new string[] {"Inbox", "Waiting", "Someday"};
            foreach (var name in names)
            {
                IEnumerable<Lists> listResults = db.Lists.Where(list => list.Title == name);
                if (listResults.Count() == 0)
                {
                    // si la requete est vide, il n'y a pas de liste avec ce nom, donc aucune tache dedans
                    defaultListsMap.Add(name, makeContext(name, ""));
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
                && x.Owner == user.Id))
            {
                IStaticList stl = createDBStaticList(list, taskMap);
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

            //selection des orphelins
            var orphans = db.Tasks.Where(item => item.Lists_Tasks.Count == 0).Select(item => taskMap[item.Id]);
            //ajout dans la inbox
            foreach (ISingleTask orphan in orphans)
                sys.Inbox.AddTask(orphan);

            return sys;
        }

        private IStaticList createDBStaticList(Lists list, IDictionary<int, ISingleTask> taskMap)
        {
            DataClassesDataContext db = dbProvider.Database;
            IStaticList stl = new DBStaticList(list.Title, list.Id, dbProvider);
            // attetion User codé en dur
            foreach (Lists_Tasks tl in db.Lists_Tasks.Where(x => x.List_id == list.Id && x.Owner == 1))
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
