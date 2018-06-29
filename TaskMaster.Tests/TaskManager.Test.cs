using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using TaskMaster.Data;
using BusinessLayer.Models;

namespace TaskMaster.Tests
{
    [TestFixture]
    public class TaskManagerTest
    {
        [Test]
        public void GetTasks()
        {
            var mockObj = new Mock<IRepository>();
            var businessLayer = new BusinessLayer.TaskManager(mockObj.Object);
            var tasks = new List<Task_Table>();
            tasks.Add(new Task_Table() { Task = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Parent_ID = 5 });
            mockObj.Setup(x => x.GetTasks()).Returns(tasks);
            //Assert
            var actualTasks = businessLayer.GetTasks();
            Assert.AreEqual(tasks.Count(), actualTasks.Count());
            Assert.AreEqual(tasks.FirstOrDefault().Task, actualTasks.FirstOrDefault().Task);

        }

        [Test]
        public void GetSpecificTask()
        {
            var mockObj = new Mock<IRepository>();
            var businessLayer = new BusinessLayer.TaskManager(mockObj.Object);
            var tasks = new Task_Table() { Task = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Parent_ID = 5 };
            
            mockObj.Setup(x => x.GetSpecificTask(It.IsAny<int>())).Returns(tasks);
            //Assert
            var actualTasks = businessLayer.GetSpecificTask(It.IsAny<int>());
            Assert.AreEqual(tasks.Task, actualTasks.Task);

        }

        [Test]
        public void EndTask()
        {
            var mockObj = new Mock<IRepository>();
            var businessLayer = new BusinessLayer.TaskManager(mockObj.Object);
            /*ar tasks = new Task_Table() { Task = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Parent_ID = 5 };*/

            //mockObj.Setup(x => x.EndTask(It.IsAny<int>())).Returns(tasks);
            //Assert
            businessLayer.EndTask(new TaskModel() { TaskId=5});
            mockObj.Verify(x => x.EndTask(It.IsAny<int>()), Times.Once);

        }

        [Test]
        public void UpdateTask()
        {
            var mockObj = new Mock<IRepository>();
            var businessLayer = new BusinessLayer.TaskManager(mockObj.Object);
            /*ar tasks = new Task_Table() { Task = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Parent_ID = 5 };*/

            //mockObj.Setup(x => x.EndTask(It.IsAny<int>())).Returns(tasks);
            //Assert
            businessLayer.UpdateTask(new TaskModel() { TaskId = 5 });
            mockObj.Verify(x => x.AddTask(It.IsAny<Task_Table>()), Times.Once);
            mockObj.Verify(x => x.DeleteTask(It.IsAny<int>()), Times.Once);

        }

        [Test]
        public void AddTask()
        {
            var mockObj = new Mock<IRepository>();
            var businessLayer = new BusinessLayer.TaskManager(mockObj.Object);
            /*ar tasks = new Task_Table() { Task = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Parent_ID = 5 };*/

            //mockObj.Setup(x => x.EndTask(It.IsAny<int>())).Returns(tasks);
            //Assert
            businessLayer.AddTask(new TaskModel() { TaskId = 5 });
            mockObj.Verify(x => x.AddTask(It.IsAny<Task_Table>()), Times.Once);
        }

        [Test]
        public void IsTaskExists()
        {
            var mockObj = new Mock<IRepository>();
            var businessLayer = new BusinessLayer.TaskManager(mockObj.Object);
            /*ar tasks = new Task_Table() { Task = "TestTest", Start_Date = DateTime.Now, End_Date = DateTime.Now.AddDays(7), Parent_ID = 5 };*/

            mockObj.Setup(x => x.IsTaskExists(It.IsAny<string>())).Returns(true);
            //Assert
            Assert.IsTrue(businessLayer.IsTaskExist("test"));
        }
    }
}
