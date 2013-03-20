using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    public interface IGTDFactory
    {
        ISingleTask makeTask(string title, string description, DateTimeOffset? DueDate, IUser owner);
        IProject makeProject(string title, string description, DateTimeOffset? DueDate, IUser owner);
        IGTDSystem makeSystem(IUser owner);
        ISchedule makeSchedule(IGTDSystem source);
        IStaticList makeContext(string title, string description, IUser owner);
    }
}
