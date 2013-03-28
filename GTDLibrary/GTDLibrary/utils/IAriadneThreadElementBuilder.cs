using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GTDLibrary
{
    internal interface IAriadneThreadElementBuilder
    {
        long MaximumElement { get; set; }
        bool ExecuteAllCallbacks { get; set; }
        AriadneThread.AriadneThreadCallback HomeCallback { get; set; }
        void MoveNext(AriadneThread.AriadneThreadCallback call, String name);
        void MoveBack();
    }
}
