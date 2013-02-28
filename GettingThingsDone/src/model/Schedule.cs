using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.ComponentModel;

using GettingThingsDone.src.model.visitor;

namespace GettingThingsDone.src.model
{
    public class Schedule
    {
        private IGTDSystem sys;

        public Schedule(IGTDSystem system)
        {
            sys = system;
        }

        public IEnumerable Today { get { 
            IEnumerable tasks = sys.accept(new AllTasks());
            ICollectionView view = CollectionViewSource.GetDefaultView(tasks);
            view.Filter = isDueToday;
            return view;
        } }

        private bool isDueToday(Object obj)
        {
            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            DateTimeOffset dueDate = t.DueDate.Value;
            return dueDate.Day.Equals(DateTimeOffset.Now.Day);
        }
    }
}
