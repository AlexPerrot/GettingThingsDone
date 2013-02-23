using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using GettingThingsDone.src.model;

namespace GettingThingsDone.src.data
{
    class DBGTDFactory : IGTDFactory
    {
        private IDatabaseProvider dbProvider;

        public DBGTDFactory(IDatabaseProvider dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public ISingleTask makeTask(string title, string description, DateTimeOffset? DueDate)
        {
            DataClassesDataContext dc = dbProvider.Database;

            Tasks dbTask = new Tasks();
            dbTask.CreationDate = DateTimeOffset.Now;
            dbTask.Title = title;
            dbTask.Description = description;
            dbTask.Owner = (App.Current as App).Admin.Id;

            dc.Tasks.InsertOnSubmit(dbTask);
            dc.SubmitChanges();

            return new DBSingleTask(dbTask);
        }

        public IProject makeProject(string title)
        {
            throw new NotImplementedException();
        }
    }
}
