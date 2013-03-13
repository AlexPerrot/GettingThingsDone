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
            foreach (Projects_Tasks project_tasks in db.Projects_Tasks)
            {
                if (project_tasks.Project_id == this.id)
                    db.Projects_Tasks.DeleteOnSubmit(project_tasks);
                foreach (Tasks task in db.Tasks)
                {
                    if (task.Id == project_tasks.Task_id)
                        db.Tasks.DeleteOnSubmit(task);
                }
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
                pt.Owner = (App.Current as App).Admin.Id;
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
        }
    }
}