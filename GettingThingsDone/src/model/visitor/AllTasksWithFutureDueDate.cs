using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model.visitor
{
    public class AllTasksWithFutureDueDate : GTDVisitor<IEnumerable<Task>>
    {

        private IEnumerable<Task> Yield(Task t)
        {
            yield return t;
        }

        public IEnumerable<Task> visit(ISingleTask t)
        {
            if (t.DueDate.HasValue 
                && t.DueDate.Value >= DateTime.Today)
                return Yield(t);
            else
                return Enumerable.Empty<Task>();
        }

        public IEnumerable<Task> visit(IProject p)
        {
            if (p.DueDate.HasValue && p.DueDate.Value >= DateTime.Now)
                return Yield(p);
            else
                return Enumerable.Empty<Task>();
        }

        public IEnumerable<Task> visit(IGTDSystem s)
        {
            IEnumerable<Task> tmp = Enumerable.Empty<Task>();
            foreach (TaskList l in s)
                tmp = tmp.Concat(l.accept(this));
            return tmp;
        }

        public IEnumerable<Task> visit(IStaticList l)
        {
            IEnumerable<Task> tmp = Enumerable.Empty<Task>();
            foreach (Task tl in l)
                tmp = tmp.Concat(tl.accept(this));
            return tmp;
        }

        public IEnumerable<Task> visit(IDynamicList l)
        {
            IEnumerable<Task> tmp = Enumerable.Empty<Task>();
            foreach (Task tl in l)
                tmp = tmp.Concat(tl.accept(this));
            return tmp;
        }
    }
}
