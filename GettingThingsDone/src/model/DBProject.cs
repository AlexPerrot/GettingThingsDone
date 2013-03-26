using System;
using System.Collections.Generic;
using System.Linq;
using GettingThingsDone.src.data;
using GettingThingsDone.src.model;
using GettingThingsDone.src.model.visitor;

namespace GettingThingsDone.src.data
{
    public class DBProject : IProject
    {
        public string Title {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                return p.Title; 
            }
            
            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                p.Title = value;
                db.SubmitChanges(); 
            } 
        }

        public string Description
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                return p.Description; 
            } 
            
            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                p.Description = value;
                db.SubmitChanges();
            }
        }
       
        public bool Done
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                return p.Done;
            } 
            
            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                p.Done = value;
                db.SubmitChanges();
            }
        }

        public DateTimeOffset? DueDate
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                return p.DueDate;
            }

            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                p.DueDate = value;
                db.SubmitChanges();
            }
        }

        public DateTimeOffset CreationDate
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                return p.CreationDate;
            } 
            
            private set
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                p.CreationDate = value;
                db.SubmitChanges();
            }
        }
       
        public void Delete()
        {
            DataClassesDataContext db = dbProvider.Database;
            Projects p = db.Projects.Single(x => x.Id == this.id);
            db.Projects.DeleteOnSubmit(p);
            foreach (Projects_Tasks project_tasks in db.Projects_Tasks.Where(item => item.Project_id == this.id))
            {
                 db.Projects_Tasks.DeleteOnSubmit(project_tasks);
            }            
            db.SubmitChanges();
        }

        public T accept<T>(GTDVisitor<T> v)
        {
            return v.visit(this);
        }

        private List<Task> tasks = new List<Task>();
        public IEnumerable<Task> Tasks
        {
            get { return tasks; }
        }

        private int id;
        private IDatabaseProvider dbProvider;

        public DBProject(Projects project, IDatabaseProvider dbProvider)
        {
            this.id = project.Id;
            this.dbProvider = dbProvider;
        }

        public void AddTask(Task t)
        {
            tasks.Add(t);

            DataClassesDataContext dc = dbProvider.Database;
            int id = dbProvider.IdManager.GetId(t);
            IEnumerable<Projects_Tasks> ptlist = dc.Projects_Tasks.Where(p => p.Task_id == id);
            if (ptlist.Count() == 0)
            {
                Projects_Tasks pt = new Projects_Tasks();
                pt.Project_id = this.id;
                pt.Task_id = id;
                pt.Owner = dbProvider.IdManager.GetId(t.Owner);
                pt.Task_order = this.Tasks.ToArray<Task>().Length-1;
                dc.Projects_Tasks.InsertOnSubmit(pt);
            }
            else
            {
                Projects_Tasks pt = ptlist.First();
                pt.Projects.Id = this.id;
            }
            dc.SubmitChanges();
        }

        public void RemoveTask(Task t)
        {
            tasks.Remove(t);
            reorderAllTasksInDB();
        }

        public void moveTaskTo(int currentIndex, int nextIndex)
        {
            if (currentIndex >= 0 && currentIndex < this.tasks.Count
                && nextIndex >= 0 && nextIndex < this.tasks.Count
                && currentIndex != nextIndex)
            {
                DataClassesDataContext db = dbProvider.Database;

                // On échange l'ordre des tâches en BD
                Projects_Tasks project_task = db.Projects_Tasks.Single(item => item.Task_id == ((DBSingleTask)(this.tasks.ElementAt(currentIndex))).Id);
                project_task.Task_order = nextIndex;

                Projects_Tasks project_task2 = db.Projects_Tasks.Single(item => item.Task_id == ((DBSingleTask)(this.tasks.ElementAt(nextIndex))).Id);
                project_task2.Task_order = currentIndex;

                db.SubmitChanges();

                // Puis dans la liste
                Task itemToMove = this.tasks.ElementAt(currentIndex);
                this.tasks.RemoveAt(currentIndex);
                this.tasks.Insert(nextIndex, itemToMove);
            }
        }

        private void reorderAllTasksInDB()
        {
            DataClassesDataContext db = dbProvider.Database;

            int task_order = 0;
            foreach (Task task in this.tasks)
            {
                Projects_Tasks project_task = db.Projects_Tasks.Single(item => item.Task_id == ((DBSingleTask)(task)).Id);
                project_task.Task_order = task_order;

                task_order++;
            }

            db.SubmitChanges();
        }

        public IUser Owner
        {
            get {
                DBIdManager mngr = this.dbProvider.IdManager;

                DataClassesDataContext db = this.dbProvider.Database;
                Projects p = db.Projects.Single(item => item.Id == this.id);

                return mngr.GetUser(p.Owner);
            }
        }
    }
}