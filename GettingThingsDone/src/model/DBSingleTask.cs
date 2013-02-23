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
                dbTask.Title = value;
                submitChanges();
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
                dbTask.Description = value;
                submitChanges();
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
                dbTask.Done = value;
                submitChanges();
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
                dbTask.DueDate = value;
                submitChanges();
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
