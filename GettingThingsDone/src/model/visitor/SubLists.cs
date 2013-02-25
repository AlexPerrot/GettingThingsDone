using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model.visitor
{
    public class SubLists : GTDVisitor<IEnumerable<TaskList>>
    {

        public IEnumerable<TaskList> visit(ISingleTask t)
        {
            return Enumerable.Empty<TaskList>();
        }

        public IEnumerable<TaskList> visit(IProject p)
        {
            return Enumerable.Empty<TaskList>();
        }

        public IEnumerable<TaskList> visit(IGTDSystem s)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskList> visit(IStaticList l)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaskList> visit(IDynamicList l)
        {
            throw new NotImplementedException();
        }
    }
}
