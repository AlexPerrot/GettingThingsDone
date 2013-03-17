using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    public interface IProject : Task
    {
        IEnumerable<Task> Tasks { get; }
        void AddTask(Task t);
        void RemoveTask(Task t);
        void moveTaskTo(int currentIndex, int nextIndex);
    }
}
