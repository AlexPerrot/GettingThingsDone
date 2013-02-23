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
                return dbTask.Title;
            }
            set
            {
                DataClassesDataContext dc = dbProvider.Database;
                dc.Tasks.Attach(dbTask);
                dbTask.Title = value;
                dc.SubmitChanges();
            }
        }

        public string Description
        {
            get
            {
               return dbTask.Description;
            }
            set
            {
                DataClassesDataContext dc = dbProvider.Database;
                dc.Tasks.Attach(dbTask);
                dbTask.Description = value;
                dc.SubmitChanges();
            }
        }

        public bool Done
        {
            get
            {
                return dbTask.Done;
            }
            set
            {
                DataClassesDataContext dc = dbProvider.Database;
                dc.Tasks.Attach(dbTask);
                dbTask.Done = value;
                dc.SubmitChanges();
            }
        }

        public DateTimeOffset? DueDate
        {
            get
            {
                return dbTask.DueDate;
            }
            set
            {
                DataClassesDataContext dc = dbProvider.Database;
                dc.Tasks.Attach(dbTask);
                dbTask.DueDate = value;
                dc.SubmitChanges();
            }
        }

        public DateTimeOffset CreationDate
        {
            get { return dbTask.CreationDate; }
        }

        public T accept<T>(TaskVisitor<T> v)
        {
            return v.visit(this);
        }

        private Tasks dbTask;
        private IDatabaseProvider dbProvider;

        public DBSingleTask(Tasks dbTask, IDatabaseProvider dbProvider)
        {
            this.dbTask = dbTask;
            this.dbProvider = dbProvider;
        }

        private void submitChanges()
        {
            this.dbProvider.Database.SubmitChanges();
        }
    }
}
