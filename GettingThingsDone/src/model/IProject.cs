using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    public interface IProject : Task
    {
        IList<Task> Tasks { get; }

    }
}
