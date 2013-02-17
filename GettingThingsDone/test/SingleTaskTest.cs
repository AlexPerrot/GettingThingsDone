using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GettingThingsDone.test
{
    [TestClass]
    public class SingleTaskTest
    {

        private static Task task;
        private static string name = "A task";
        private static string desc = "A description\nwith newlines";
        private static DateTimeOffset date = DateTimeOffset.Now;

        [TestInitialize]
        public void setUp()
        {
            task = new SingleTask(name, desc, date);
        }

        [TestMethod]
        public void testCreation()
        {
            Assert.IsNotNull(task);
            Assert.AreEqual(name, task.Title);
            Assert.AreEqual(desc, task.Description);
            Assert.AreEqual(date, task.DueDate);
            Assert.AreEqual(true, task.CreationDate < DateTimeOffset.Now);
        }

        [TestMethod]
        public void testName() 
        {
            task.Title = name;
            Assert.AreEqual(name, task.Title);
        }

        [TestMethod]
        public void testDesc()
        {
            string desc2 = "une description";
            task.Description = desc2;
            Assert.AreEqual(desc2, task.Description);
        }

        [TestMethod]
        public void testDueDate()
        {
            DateTimeOffset date2 = DateTimeOffset.Now;
            task.DueDate = date2;
            Assert.AreEqual(true, task.DueDate.HasValue);
            Assert.AreEqual(date2, task.DueDate);

            task = new SingleTask();
            Assert.AreEqual(false, task.DueDate.HasValue);
        }
    }

    [TestClass]
    public class ProjectTest
    {
        private static Project projet;
        private static Task task1;
        private static Task task2;

        [TestInitialize]
        public void setUp()
        {
            projet = new Project();
            task1 = new SingleTask("Tache 1", "C'est la tache 1");
            task2 = new SingleTask("Tache 2", "C'est la tache 2", DateTimeOffset.MaxValue);
        }

        [TestMethod]
        public void testNotNull()
        {
            Assert.IsNotNull(projet);
            Assert.IsNotNull(task1);
            Assert.IsNotNull(task2);
        }

        [TestMethod]
        public void testAddTask()
        {
            Assert.IsTrue(projet.Tasks.Count == 0);
            projet.Tasks.Add(task1);
            Assert.IsTrue(projet.Tasks.Count == 1);
            projet.Tasks.Add(task2);
            Assert.IsTrue(projet.Tasks.Count == 2);
        }
    }
}
