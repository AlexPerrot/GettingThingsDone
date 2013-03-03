using System;
using System.Collections.Generic;
using System.Linq;
using GettingThingsDone.src.data;
using GettingThingsDone.src.model;
using GettingThingsDone.src.model.visitor;

namespace GettingThingsDone.model
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
            get
            {
               return accept(new AllTasksFromProject(this));
            }

            private set
            {
                DataClassesDataContext db = dbProvider.Database;
                Projects p = db.Projects.Single(x => x.Id == id);
                // TODO: Pour chaque nouvelle tache, faire l'ajout en bdd
                db.SubmitChanges();
            }
        }

        private int id;
        private IDatabaseProvider dbProvider;

        public DBProject(int id, IDatabaseProvider dbProvider)
        {
            this.id = id;
            this.dbProvider = dbProvider;
            this.CreationDate = DateTimeOffset.Now;
        }
    }
}