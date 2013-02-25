using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingThingsDone.src.model.visitor
{

    public sealed class Void
    {
        private Void() { }

        public static Void Default { get { return new Void(); } }

    }

    public abstract class VoidVisitor : GTDVisitor<Void>
    {
        protected abstract void visitVoid(ISingleTask t);
        protected abstract  void visitVoid(IProject p);
        protected abstract void visitVoid(IGTDSystem s);
        protected abstract void visitVoid(IStaticList l);
        protected abstract void visitVoid(IDynamicList l);


        public Void visit(ISingleTask t)
        {
            visitVoid(t);
            return Void.Default;
        }

        public Void visit(IProject p)
        {
            visitVoid(p);
            return Void.Default;
        }

        public Void visit(IGTDSystem s)
        {
            visitVoid(s);
            return Void.Default;
        }

        public Void visit(IStaticList l)
        {
            visitVoid(l);
            return Void.Default;
        }

        public Void visit(IDynamicList l)
        {
            visitVoid(l);
            return Void.Default;
        }
    }
}
