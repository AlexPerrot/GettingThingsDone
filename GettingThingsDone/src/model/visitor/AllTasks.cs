using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model.visitor
{
    public class AllTasks : GTDVisitor<IEnumerable<Task>>
    {

        public IEnumerable<Task> visit(ISingleTask t)
        {
            yield return t;
        }

        public IEnumerable<Task> visit(IProject p)
        {
            yield return p;
        }

        public IEnumerable<Task> visit(IGTDSystem s)
        {
            IEnumerable<Task> tmp = new List<Task>();
            foreach (GTDItem i in s)
                tmp = tmp.Concat(i.accept(this));
            return tmp;
        }

        public IEnumerable<Task> visit(IStaticList l)
        {
            IEnumerable<Task> tmp = new List<Task>();
            foreach (GTDItem i in l)
                tmp = tmp.Concat(i.accept(this));
            return tmp;
        }

        public IEnumerable<Task> visit(IDynamicList l)
        {
            IEnumerable<Task> tmp = new List<Task>();
            foreach (GTDItem i in l)
                tmp = tmp.Concat(i.accept(this));
            return tmp;
        }
    }
}
