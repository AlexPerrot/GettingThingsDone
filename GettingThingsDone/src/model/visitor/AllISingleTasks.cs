using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model.visitor
{
    public class AllISingleTasks : GTDVisitor<IEnumerable<ISingleTask>>
    {

        public IEnumerable<ISingleTask> visit(ISingleTask t)
        {
            yield return t;
        }

        public IEnumerable<ISingleTask> visit(IProject p)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ISingleTask> visit(IGTDSystem s)
        {
            IEnumerable<ISingleTask> tmp = new LinkedList<ISingleTask>();
            foreach (GTDItem i in s)
                tmp = tmp.Concat(i.accept(this));
            return tmp;
        }

        public IEnumerable<ISingleTask> visit(IStaticList l)
        {
            IEnumerable<ISingleTask> tmp = new LinkedList<ISingleTask>();
            foreach (GTDItem i in l)
                tmp = tmp.Concat(i.accept(this));
            return tmp;
        }

        public IEnumerable<ISingleTask> visit(IDynamicList l)
        {
            IEnumerable<ISingleTask> tmp = new LinkedList<ISingleTask>();
            foreach (GTDItem i in l)
                tmp = tmp.Concat(i.accept(this));
            return tmp;
        }
    }
}
