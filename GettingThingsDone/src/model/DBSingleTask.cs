﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GettingThingsDone.src.model;
using GettingThingsDone.src.model.visitor;

namespace GettingThingsDone.src.data
{
    class DBSingleTask : ISingleTask, INotifyPropertyChanged
    {
        public string Title
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                return t.Title;
            }
            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                t.Title = value;
                db.SubmitChanges();
                OnPropertyChanged("Title");
            }
        }

        public string Description
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                return t.Description;
            }
            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                t.Description = value;
                db.SubmitChanges();
                OnPropertyChanged("Description");
            }
        }

        public bool Done
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                return t.Done;
            }
            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                t.Done = value;
                db.SubmitChanges();
            }
        }

        public DateTimeOffset? DueDate
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                return t.DueDate;
            }
            set
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                t.DueDate = value;
                db.SubmitChanges();
            }
        }

        public DateTimeOffset CreationDate
        {
            get
            {
                DataClassesDataContext db = dbProvider.Database;
                Tasks t = db.Tasks.Single(x => x.Id == id);
                return t.CreationDate;
            }
        }

        public T accept<T>(GTDVisitor<T> v)
        {
            return v.visit(this);
        }

        private int id;
        public int Id { get { return id; } }
        private IDatabaseProvider dbProvider;
        private IUser owner;

        public DBSingleTask(Tasks dbTask, IDatabaseProvider dbProvider)
        {
            this.id = dbTask.Id;
            this.owner = dbProvider.IdManager.GetUser(dbTask.Owner);
            this.dbProvider = dbProvider;
        }

        private void submitChanges()
        {
            this.dbProvider.Database.SubmitChanges();
        }

        public void Delete()
        {
            //obtention db
            DataClassesDataContext db = dbProvider.Database;

            //suppression des instances dans les listes
            IEnumerable<Lists_Tasks> lists = db.Lists_Tasks.Where(item => item.Task_id == this.id);
            db.Lists_Tasks.DeleteAllOnSubmit(lists);

            //suppression des instances dans les projets
            IEnumerable<Projects_Tasks> projects = db.Projects_Tasks.Where(item => item.Task_id == this.id);
            db.Projects_Tasks.DeleteAllOnSubmit(projects);
            
            //suppression de la tache proprement dite
            Tasks t = db.Tasks.Single(x => x.Id == this.id);
            db.Tasks.DeleteOnSubmit(t);

            //on poste les changements
            db.SubmitChanges();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public IUser Owner
        {
            get { return owner; }
        }
    }
}
