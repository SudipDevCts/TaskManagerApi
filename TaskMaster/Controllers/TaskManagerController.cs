using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Models;

namespace TaskMaster.Controllers
{
    [RoutePrefix("api")]
    public class TaskManagerController : ApiController
    {


        // GET: api/TaskManager/5
        [HttpGet]
        [Route("GetTask")]
        public List<TaskModel> GetTask()
        {
            TaskManager obj = new TaskManager();
            return obj.GetTasks().ToList();
        }

        [HttpGet]
        [Route("Task/{taskId}")]
        public TaskModel Task(int taskId)
        {
            TaskManager obj = new TaskManager();
            var response = obj.GetSpecificTask(taskId);
            return response;
        }

        // POST: api/TaskManager
        [HttpPost]
        [Route("AddTask")]
        public void AddTask(TaskModel task)
        {
            TaskManager obj = new TaskManager();
            obj.AddTask(task);
        }

        [HttpPut]
        [Route("EndTask")]
        public void EndTask(TaskModel task)
        {
            TaskManager obj = new TaskManager();
            obj.EndTask(task);
        }

        [HttpPost]
        [Route("UpdateTask")]
        public void UpdateTask(TaskModel task)
        {
            TaskManager obj = new TaskManager();
            obj.UpdateTask(task);

        }


    }
}
