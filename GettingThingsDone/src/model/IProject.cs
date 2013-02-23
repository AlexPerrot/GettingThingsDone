using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model
{
    interface IProject : Task
    {
        public IList<Task> Tasks { get; }

    }
}
