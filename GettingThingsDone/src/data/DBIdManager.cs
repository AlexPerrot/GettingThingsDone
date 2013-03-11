using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.data
{
    public class DBIdManager
    {
        private IDatabaseProvider dbProvider;

        public DBIdManager(IDatabaseProvider database)
        {
            this.dbProvider = database;
        }

        public int GetId(Task t)
        {
            if (t is DBSingleTask)
                return (t as DBSingleTask).Id;
            else
            {
                DataClassesDataContext dc = dbProvider.Database;
                Tasks DBT = dc.Tasks.Where(i => i.Title == t.Title && i.Description == t.Description && t.CreationDate == i.CreationDate).First();
                return DBT.Id;
            }
        }
    }
}
