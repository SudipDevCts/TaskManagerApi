using NUnit.Framework;
using System;
using System.Linq;

namespace TaskMaster.Tests
{
    [TestFixture]
    public class TaskManagerControllerTest
    {
        [Test]
        public void AddTask()
        {
            // Prepare
            var controller = new TaskMaster.Controllers.TaskManagerController();
            var task = new BusinessLayer.Models.TaskModel() { Task = "Test1", ParentTask = "Test2", Priority = 10, StartDate = System.DateTime.Now.ToString(), EndDate = System.DateTime.Now.AddDays(7).ToString() };
           
            // Act on Test  
            controller.AddTask(task);

            // Assert the result  
            var obj = new BusinessLayer.TaskManager();
            var tasks = obj.GetTasks();
            Assert.IsTrue(tasks.Any(x=>x.Task == "Test1"));
        }
        [Test]
        public void GetTasks()
        {
            // Prepare
            var controller = new TaskMaster.Controllers.TaskManagerController();
            var tasks = new BusinessLayer.TaskManager().GetTasks();

            //Act
            var response  = controller.GetTask();

            // Assert
            Assert.IsTrue(tasks.Count()== response.Count());

        }

        [Test]
        public void GetSpecificTask()
        {
            // Prepare
            var controller = new TaskMaster.Controllers.TaskManagerController();
            var task = new BusinessLayer.TaskManager().GetTasks().FirstOrDefault();
           

            //Act
            var response = controller.Task(task.TaskId);

            // Assert
            Assert.IsTrue(task.Task==response.Task);

        }

        [Test]
        public void EndTask()
        {
            // Prepare
            var controller = new TaskMaster.Controllers.TaskManagerController();
            var date = System.DateTime.Now.Date;
            var task = new BusinessLayer.TaskManager().GetTasks().FirstOrDefault(x=>x.EndDate.Substring(0, 9) != date.ToString().Substring(0, 9));


            //Act
            controller.EndTask(task);

            // Assert
            var endedTask = new BusinessLayer.TaskManager().GetSpecificTask(task.TaskId).EndDate;
          
            Assert.IsTrue(endedTask.Substring(0, 9) == date.ToString().Substring(0, 9));

        }

        [Test]
        public void UpdateTask()
        {
            // Prepare
            var controller = new TaskMaster.Controllers.TaskManagerController();
            var obj = new BusinessLayer.TaskManager();
            var task = obj.GetTasks().FirstOrDefault();
            task.Task = task.Task + "updated";


            //Act
            controller.UpdateTask(task);

            // Assert
            
            Assert.IsTrue(obj.IsTaskExist(task.Task));

        }
    }
}
