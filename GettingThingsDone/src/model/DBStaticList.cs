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
        private IUser owner;
        private ObservableCollection<GTDItem> list = new ObservableCollection<GTDItem>();
        public ObservableCollection<GTDItem> List { get { return list; } }

        private int id;
        private IDatabaseProvider dbProvider;

        public String Name { get; set; }

        public DBStaticList(Lists l, IDatabaseProvider dbProvider)
        {
            this.Name = l.Title;
            this.id = l.Id;
            this.owner = dbProvider.IdManager.GetUser(l.Owner);
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
            
            Lists_Tasks lt = db.Lists_Tasks.Single(x => x.List_id == this.id && x.Task_id == idm.GetId(t) 
                && x.Owner == 1);
            db.Lists_Tasks.DeleteOnSubmit(lt);

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
                dbListTask.Owner = idm.GetId(t.Owner);

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
            // do nothing
        }


        public IUser Owner
        {
            get {
                return owner;
            }
        }
    }
}
