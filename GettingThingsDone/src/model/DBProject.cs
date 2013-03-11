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
            db.SubmitChanges();
        }

        public T accept<T>(GTDVisitor<T> v)
        {
            return v.visit(this);
        }

        public IList<Task> Tasks
        {
            get;

            private set;
        }

        private int id;
        private IDatabaseProvider dbProvider;

        public DBProject(Projects project, IDatabaseProvider dbProvider)
        {
            this.id = project.Id;
            this.dbProvider = dbProvider;
            Tasks = new List<Task>();
        }
    }
}