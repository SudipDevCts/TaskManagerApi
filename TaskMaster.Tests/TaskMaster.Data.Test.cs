using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using TaskMaster.Data;
using System.Data.Entity;

namespace TaskMaster.Tests
{
    [TestFixture]
    public class TaskMasterDataTests
    {
        [Test]
        public void GetTasksData()
        {
            var data = new List<Task_Table>
            {
                new Task_Table { Task = "BBB" },
                new Task_Table { Task = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Task_Table>>();
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TaskManagerEntities>();
            mockContext.Setup(c => c.Task_Table.Include("ParentTask_Table")).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            var tasks = service.GetTasks();

            Assert.AreEqual(data.Count(), tasks.Count());
            Assert.AreEqual(data.First().Task, tasks.First().Task);
        }



        [Test]
        public void GetSpecificTasksData()
        {
            var data = new List<Task_Table>
            {
                new Task_Table { Task = "BBB", Task_ID=5 },
                new Task_Table { Task = "ZZZ" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Task_Table>>();
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TaskManagerEntities>();
            mockContext.Setup(c => c.Task_Table.Include("ParentTask_Table")).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            var task = service.GetSpecificTask(5);

            Assert.AreEqual(data.First().Task, task.Task);
            //Assert.AreEqual(data.First().Task, tasks.First().Task);
        }


        [Test]
        public void AddTaskData()
        {
            var mockSet = new Mock<DbSet<Task_Table>>();

            var mockContext = new Mock<TaskManagerEntities>();
            mockContext.Setup(m => m.Task_Table).Returns(mockSet.Object);

            var service = new Repository(mockContext.Object);
            service.AddTask(new Task_Table() { });

            mockSet.Verify(m => m.Add(It.IsAny<Task_Table>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void EndTaskData()
        {
            var mockSet = new Mock<DbSet<Task_Table>>();
            var data = new List<Task_Table>
            {
                new Task_Table { Task = "BBB", Task_ID=5 },
                new Task_Table { Task = "ZZZ" },

            }.AsQueryable();


            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TaskManagerEntities>();
            mockContext.Setup(c => c.Task_Table).Returns(mockSet.Object);



            var service = new Repository(mockContext.Object);
            service.EndTask(It.IsAny<int>());

            //mockSet.Verify(m => m.Add(It.IsAny<Task_Table>()), Times.Once());
            //mockSet.Verify(m => m.Remove(It.IsAny<Task_Table>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Test]
        public void UpdateTaskData()
        {
            var mockSet = new Mock<DbSet<Task_Table>>();
            var data = new List<Task_Table>
            {
                new Task_Table { Task = "BBB", Task_ID=5 },
                new Task_Table { Task = "ZZZ" },

            }.AsQueryable();

          
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Task_Table>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<TaskManagerEntities>();
            mockContext.Setup(c => c.Task_Table).Returns(mockSet.Object);
            
           

            var service = new Repository(mockContext.Object);
            service.DeleteTask(It.IsAny<int>());

            //mockSet.Verify(m => m.Add(It.IsAny<Task_Table>()), Times.Once());
            mockSet.Verify(m => m.Remove(It.IsAny<Task_Table>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}




