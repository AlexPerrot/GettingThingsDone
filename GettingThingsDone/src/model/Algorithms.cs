using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    class Algorithms
    {
        public static bool getDueToday(GTDItem item)
        {
            Task t = (Task)item;
            //if (t.DueDate == DateTime.Today)
            if (t.DueDate != null &&
                ((DateTimeOffset)t.DueDate).Day == DateTimeOffset.Now.Day &&
                    ((DateTimeOffset)t.DueDate).Month == DateTimeOffset.Now.Month &&
                    ((DateTimeOffset)t.DueDate).Year == DateTimeOffset.Now.Year)
            {
                return true;
            }

            return false;
        }

        public static bool getDueTomorrow(GTDItem item)
        {
            Task t = (Task)item;
            if (t.DueDate != null &&
                ((DateTimeOffset)t.DueDate).Day == DateTimeOffset.Now.AddDays(1).Day &&
                    ((DateTimeOffset)t.DueDate).Month == DateTimeOffset.Now.AddDays(1).Month &&
                    ((DateTimeOffset)t.DueDate).Year == DateTimeOffset.Now.AddDays(1).Year)
            {
                return true;
            }

            return false;
        }

        public static bool getDueNextWeek(GTDItem item)
        {
            /*Task t = (Task)item;
            if (t.DueDate != null &&
                ((DateTimeOffset)t.DueDate).Day == DateTimeOffset.Now.AddDays(1).Day &&
                    ((DateTimeOffset)t.DueDate).Month == DateTimeOffset.Now.AddDays(1).Month &&
                    ((DateTimeOffset)t.DueDate).Year == DateTimeOffset.Now.AddDays(1).Year)
            {
                return true;
            }*/

            return false;
        }

        public static bool getDueNextMonth(GTDItem item)
        {
            /*Task t = (Task)item;
            if (t.DueDate != null &&
                ((DateTimeOffset)t.DueDate).Day == DateTimeOffset.Now.AddDays(1).Day &&
                    ((DateTimeOffset)t.DueDate).Month == DateTimeOffset.Now.AddDays(1).Month &&
                    ((DateTimeOffset)t.DueDate).Year == DateTimeOffset.Now.AddDays(1).Year)
            {
                return true;
            }*/

            return false;
        }
    }
}
