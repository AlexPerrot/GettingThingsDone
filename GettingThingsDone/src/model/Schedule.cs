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
    public static class Ext
    {
        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }
    }

    public class Schedule
    {
        private IGTDSystem sys;

        public Schedule(IGTDSystem system)
        {
            sys = system;
        }

        public IEnumerable Today
        {
            get
            {
                IEnumerable tasks = sys.accept(new AllTasks());
                ICollectionView view = CollectionViewSource.GetDefaultView(tasks);
                view.Filter = isDueToday;
                return view;
            }
        }

        public IEnumerable Tomorrow
        {
            get
            {
                IEnumerable tasks = sys.accept(new AllTasks());
                ICollectionView view = CollectionViewSource.GetDefaultView(tasks);
                view.Filter = isDueTomorrow;
                return view;
            }
        }

        public IEnumerable ThisWeek
        {
            get
            {
                IEnumerable tasks = sys.accept(new AllTasks());
                ICollectionView view = CollectionViewSource.GetDefaultView(tasks);
                view.Filter = isDueThisWeek;
                return view;
            }
        }


        private bool isDueToday(Object obj)
        {
            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            DateTimeOffset dueDate = t.DueDate.Value;
            return dueDate.Day.Equals(DateTimeOffset.Now.Day);
        }

        private bool isDueTomorrow(Object obj)
        {
            if (isDueToday(obj)) return false;

            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            DateTimeOffset dueDate = t.DueDate.Value;
            return dueDate.Day.Equals(DateTimeOffset.Now.AddDays(1).Day);
        }

        private bool isDueThisWeek(Object obj) {
            if (isDueToday(obj) || isDueTomorrow(obj)) return false;

            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            DateTimeOffset dueDate = t.DueDate.Value;
            DateTimeOffset nextMonday = DateTime.Now.Next(DayOfWeek.Monday);
            return dueDate < nextMonday;
        }

  
    }
}
