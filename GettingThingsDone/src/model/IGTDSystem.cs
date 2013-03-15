using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    public interface IGTDSystem : TaskList
    {
        TaskList Inbox { get; }
        List<Task> Projects { get; }
        TaskList Someday { get; }
        TaskList Waiting { get; }
        ObservableCollection<TaskList> Contexts { get; }

    }
}
