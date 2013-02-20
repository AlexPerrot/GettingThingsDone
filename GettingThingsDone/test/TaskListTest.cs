using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GettingThingsDone.test
{
    [TestClass]
    public class TaskListTest
    {
        StaticList List;
        String ListName = "List";
        GTDItem item = new SingleTask();

        [TestInitialize]
        public void setUp()
        {
            List = new StaticList(ListName);
        }

        [TestMethod]
        public void testCreationList()
        {
            Assert.IsNotNull(List);
            Assert.AreEqual(ListName, List.Name);
        }

        [TestMethod]
        public void testAddList()
        {
            Assert.AreEqual(List.list.Count,0);
            List.AddItem(item);
            Assert.IsTrue(List.list.Contains(item));
        }

        [TestMethod]
        public void testRemoveList()
        {
            Assert.AreEqual(List.list.Count, 0);
            List.AddItem(item);
            Assert.IsTrue(List.list.Contains(item));
            List.RemoveItem(item);
            Assert.IsFalse(List.list.Contains(item));
        }

        [TestMethod]
        public void StaticListToString()
        {
            Assert.AreEqual(List.ToString(), ListName);
        }
    }

    [TestClass]
    public class GTDSystemTest
    {
        GTDSystem system;

        [TestInitialize]
        public void setUp()
        {
            system = new GTDSystem();
        }

        [TestMethod]
        public void CreationGTDSystem()
        {
            Assert.IsNotNull(system.Today);
            Assert.IsNotNull(system.Tomorrow);
            Assert.IsNotNull(system.Someday);
            Assert.IsNotNull(system.Waiting);
            Assert.IsNotNull(system.Contexts);
            Assert.IsNotNull(system.Inbox);
            Assert.AreEqual(system.Today.Name, "Today");
            Assert.AreEqual(system.Tomorrow.Name, "Tomorrow");
            Assert.AreEqual(system.Someday.Name, "Someday");
            Assert.AreEqual(system.Waiting.Name, "Waiting");
        }
    }
}
