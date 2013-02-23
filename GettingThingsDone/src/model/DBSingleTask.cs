using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingThingsDone.src.model;

namespace GettingThingsDone.src.data
{
    class DBSingleTask : ISingleTask
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

        public T accept<T>(TaskVisitor<T> v)
        {
            return v.visit(this);
        }

        private int id;
        private IDatabaseProvider dbProvider;

        public DBSingleTask(Tasks dbTask, IDatabaseProvider dbProvider)
        {
            this.id = dbTask.Id;
            this.dbProvider = dbProvider;
        }

        private void submitChanges()
        {
            this.dbProvider.Database.SubmitChanges();
        }
    }
}
