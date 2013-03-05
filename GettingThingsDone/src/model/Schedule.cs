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
    public static class DateTimeHelper
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

    public class Schedule : GettingThingsDone.src.model.ISchedule, INotifyPropertyChanged
    {
        private IGTDSystem sys;
        private System.Collections.Generic.IEnumerable<Task> tasks;

        public Schedule(IGTDSystem system)
        {
            sys = system;
            tasks = sys.accept(new AllTasksWithFutureDueDate());
        }

        private ICollectionView today;
        public IEnumerable Today
        {
            get
            {
                if (today == null)
                    today = filterView(isDueToday);
                return today;
            }
        }

        private ICollectionView tomorrow;
        public IEnumerable Tomorrow
        {
            get
            {
                if (tomorrow == null)
                    tomorrow = filterView(isDueTomorrow);
                return tomorrow;
            }
        }

        private ICollectionView thisWeek;
        public IEnumerable ThisWeek
        {
            get
            {
                if (thisWeek == null)
                    thisWeek = filterView(isDueThisWeek);
                return thisWeek;
            }
        }

        private ICollectionView thisMonth;
        public IEnumerable ThisMonth
        {
            get
            {
                if (thisMonth == null)
                    thisMonth = filterView(isDueThisMonth);
                return thisMonth;
            }
        }

        private ICollectionView nextMonth;
        public IEnumerable NextMonth
        {
            get
            {
                if (nextMonth == null)
                    nextMonth = filterView(isDueNextMonth);
                return nextMonth;
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

        private bool isDueThisMonth(Object obj)
        {
            if (isDueToday(obj) || isDueTomorrow(obj) || isDueThisWeek(obj)) return false;

            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            DateTimeOffset dueDate = t.DueDate.Value;
            return dueDate.Month < DateTime.Now.AddMonths(1).Month;
        }

        private bool isDueNextMonth(Object obj)
        {
            if (isDueToday(obj) || isDueTomorrow(obj) || isDueThisWeek(obj) || isDueThisMonth(obj)) return false;

            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            DateTimeOffset dueDate = t.DueDate.Value;
            return dueDate.Month < DateTime.Now.AddMonths(2).Month;
        }


        private ICollectionView filterView(Predicate<Object> pred)
        {
            ICollectionView view = new CollectionViewSource() { Source = tasks }.View;
            view.Filter = pred;
            view.GroupDescriptions.Add(new ContextGroupDescription(sys));
            return view;
        }

        public void update()
        {
            this.tasks = sys.accept(new AllTasksWithFutureDueDate());
            today = filterView(isDueToday);
            tomorrow = filterView(isDueTomorrow);
            thisWeek = filterView(isDueThisWeek);
            thisMonth = filterView(isDueThisMonth);
            nextMonth = filterView(isDueNextMonth);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Today"));
                PropertyChanged(this, new PropertyChangedEventArgs("Tomorrow"));
                PropertyChanged(this, new PropertyChangedEventArgs("ThisWeek"));
                PropertyChanged(this, new PropertyChangedEventArgs("ThisMonth"));
                PropertyChanged(this, new PropertyChangedEventArgs("NextMonth"));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }

    class ContextGroupDescription : PropertyGroupDescription
    {
        private IGTDSystem sys;

        public ContextGroupDescription(IGTDSystem system) : base()
        {
            sys = system;
        }

        private String findContext(Task t)
        {
            foreach (TaskList l in sys)
                if (l.Contains(t))
                    return l.Name;
            return ""; 
        }

        public override object GroupNameFromItem(object item, int level,
                                                System.Globalization.CultureInfo culture)
        {
            object value = base.GroupNameFromItem(item, level, culture);

            Task t = item as Task;
            if (t != null)
                return findContext(t);

            return value;
        } 
    }
}
