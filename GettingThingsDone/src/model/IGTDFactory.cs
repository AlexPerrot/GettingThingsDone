using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    public interface IGTDFactory
    {
        ISingleTask makeTask(string title, string description, DateTimeOffset? DueDate);
        IProject makeProject(string title);
        GTDSystem makeSystem();
        void updateSchedule(GTDSystem sys);
    }
}
