using GettingThingsDone.src.model;
using GettingThingsDone.src.model.visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.data
{
    class DBStaticList : IStaticList
    {
        private ObservableCollection<GTDItem> list = new ObservableCollection<GTDItem>();
        public ObservableCollection<GTDItem> List { get { return list; } }

        private int id;
        private IDatabaseProvider dbProvider;

        public String Name { get; set; }

        public DBStaticList(String name, int id, IDatabaseProvider dbProvider)
        {
            this.Name = name;
            this.id = id;
            this.dbProvider = dbProvider;
        }

    	public virtual IEnumerator<GTDItem> GetEnumerator()
	    {
            return list.GetEnumerator();
	    }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

	    public virtual void AddItem(GTDItem item)
	    {
            list.Add(item);
        }

	    public virtual void RemoveItem(GTDItem item)
	    {
            list.Remove(item);
	    }

	    public virtual T accept<T>(GTDVisitor<T> v)
	    {
            return v.visit(this);
        }

        public override string ToString()
        {
            return this.Name;
        }

        public void removeTask(Task t)
        {
            DataClassesDataContext db = dbProvider.Database;
            DBIdManager idm = dbProvider.IdManager;
            
            var ltenum = db.Lists_Tasks.Where(x => x.List_id == this.id && x.Task_id == idm.GetId(t) 
                && x.Owner == 1);
            foreach (var lt in ltenum)
            {
                db.Lists_Tasks.DeleteOnSubmit(lt);
            }
            //on poste les changements
            db.SubmitChanges();

            list.Remove(t);
        }


        public void AddTask(Task t)
        {
            DataClassesDataContext db = dbProvider.Database;
            DBIdManager idm = dbProvider.IdManager;

            if (db.Lists_Tasks.Count(x => x.List_id == this.id && x.Task_id == idm.GetId(t)) == 0)
            {
                Lists_Tasks dbListTask = new Lists_Tasks();
                dbListTask.Task_id = idm.GetId(t);
                dbListTask.List_id = this.id;
                dbListTask.Owner = (App.Current as App).Admin.Id;

                db.Lists_Tasks.InsertOnSubmit(dbListTask);
                db.SubmitChanges();
            }

            this.AddItem(t);
        }

        public void AddSubList(TaskList l)
        {
            this.AddItem(l);
        }

        public void removeSubList(TaskList l)
        {
            this.RemoveItem(l);
        }


        public void Delete()
        {
            DataClassesDataContext db = dbProvider.Database;
            Lists dbo = db.Lists.Single(item => item.Id == this.id);
            db.Lists.DeleteOnSubmit(dbo);
            db.SubmitChanges();
        }
    }
}
