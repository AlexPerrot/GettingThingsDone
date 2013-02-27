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
            if (t.DueDate == DateTime.Today)
                return true;
            return false;
        }
    }
}
