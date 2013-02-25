using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model.visitor
{
    public interface GTDVisitor<T>
    {
        T visit(ISingleTask t);
        T visit(IProject p);
        T visit(IGTDSystem s);
        T visit(IStaticList l);
        T visit(IDynamicList l);
    }
}
