using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GettingThingsDone.test
{
    [TestClass]
    class SingleTaskTest
    {
        [TestMethod]
        public void testCreation()
        {
            Task st = new SingleTask();
        }
    }
}
