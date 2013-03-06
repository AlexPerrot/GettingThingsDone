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
        public static DateTime Tomorrow { get { return DateTime.Today.AddDays(1); } }
        public static DateTime NextMonday { get { return DateTime.Today.Next(DayOfWeek.Monday); } }
        public static DateTime NextFirst
        {
            get
            {
                DateTime today = DateTime.Today;
                return new DateTime(today.Year, today.Month + 1, 1);
            }
        }

        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }
    }

    public static class TaskHelper
    {
        public static bool IsDueAt(this Task t, DateTime date)
        {
            return t.DueDate == date;
        }

        public static bool IsDueBefore(this Task t, DateTime date)
        {
            return t.DueDate < date;
        }
        
        public static bool IsDueAfter(this Task t, DateTime date)
        {
            return t.DueDate > date;
        }
    }

    public class Schedule : GettingThingsDone.src.model.ISchedule, INotifyPropertyChanged
    {
        private IGTDSystem sys;
        private System.Collections.ObjectModel.ObservableCollection<Task> tasks;

        public Schedule(IGTDSystem system)
        {
            sys = system;
            tasks = new System.Collections.ObjectModel.ObservableCollection<Task>(sys.accept(new AllTasksWithFutureDueDate()));
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
            System.Console.WriteLine("DueToday");
            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            return t.IsDueAt(DateTime.Today);
        }

        private bool isDueTomorrow(Object obj)
        {
            System.Console.WriteLine("DueTomorrow");

            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            return t.IsDueAt(DateTimeHelper.Tomorrow);
        }

        private bool isDueThisWeek(Object obj) {
            System.Console.WriteLine("DueThisWeek");

            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            return t.IsDueAfter(DateTimeHelper.Tomorrow) && t.IsDueBefore(DateTimeHelper.NextMonday);
        }

        private bool isDueThisMonth(Object obj)
        {
            System.Console.WriteLine("DueThisMonth");


            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            return t.IsDueAfter(DateTimeHelper.NextMonday.AddDays(-1)) && t.IsDueBefore(DateTimeHelper.NextFirst);
        }

        private bool isDueNextMonth(Object obj)
        {
            System.Console.WriteLine("DueNextMonth");

            Task t = obj as Task;
            if (t == null) return false;

            if (!t.DueDate.HasValue) return false;
            return t.IsDueAfter(DateTimeHelper.NextFirst.AddDays(-1)) && t.IsDueBefore(DateTimeHelper.NextFirst.AddMonths(1));
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
            IEnumerable tmp = sys.accept(new AllTasksWithFutureDueDate());
            tasks.Clear();
            foreach (Task t in tmp)
                tasks.Add(t);
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
