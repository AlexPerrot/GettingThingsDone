using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
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

        public DBSingleTask(Tasks dbTask)
        {
            this.dbTask = dbTask;
        }
    }
}
