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
                return filterView(isDueToday);
            }
        }

        public IEnumerable Tomorrow
        {
            get
            {
                return filterView(isDueTomorrow);
            }
        }

        public IEnumerable ThisWeek
        {
            get
            {
                return filterView(isDueThisWeek);
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


        private IEnumerable filterView(Predicate<Object> pred)
        {
            IEnumerable tasks = sys.accept(new AllTasks());
            ICollectionView view = CollectionViewSource.GetDefaultView(tasks);
            view.Filter = pred;
            return view;
        }
    }
}
