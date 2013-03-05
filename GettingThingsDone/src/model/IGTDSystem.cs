using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    public interface IGTDSystem : TaskList
    {
        TaskList Inbox { get; }
        TaskList Someday { get; }
        TaskList Waiting { get; }
        List<TaskList> Contexts { get; }

    }
}
