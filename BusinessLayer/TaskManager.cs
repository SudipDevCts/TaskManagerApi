using System;
using BusinessLayer.Models;
using TaskMaster.Data;
using System.Collections;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class TaskManager
    {
        public void AddTask(TaskModel taskDto)
        {
            Repository repository = new Repository();
            var task = new Task_Table();
            task.Task = taskDto.Task;
            if (!string.IsNullOrEmpty(taskDto.ParentTask))
            {
                ParentTask_Table ptask = new ParentTask_Table();
                ptask.Parent_Task = taskDto.ParentTask;
                task.ParentTask_Table = ptask;

            }
                
            task.Priority = taskDto.Priority;
            task.Start_Date = Convert.ToDateTime(taskDto.StartDate);
            task.End_Date = Convert.ToDateTime(taskDto.EndDate);
            repository.AddTask(task);
        }

        public IList<TaskModel> GetTasks()
        {
            Repository repository = new Repository();
            var taskDtos = new List<TaskModel>();
            var tasks = repository.GetTasks();
            foreach(var task in tasks)
            {
                var taskDto = new TaskModel();
                taskDto.TaskId = task.Task_ID;
                taskDto.Task = task.Task;
                taskDto.Priority = task.Priority.GetValueOrDefault();
                taskDto.StartDate = task.Start_Date.ToString();
                taskDto.EndDate = task.End_Date.ToString();
                taskDto.IsEditable = task.End_Date != null ? !(task.End_Date.Value.Date <= DateTime.Now.Date) : true;
                taskDto.ParentTask = task.ParentTask_Table != null ? task.ParentTask_Table.Parent_Task : "This Task has no parent";
                taskDto.ParentTaskId = task.ParentTask_Table != null ? task.ParentTask_Table.Parent_ID : 0;
                taskDtos.Add(taskDto);
            }
            return taskDtos;
        }

        public TaskModel GetSpecificTask(int taskId)
        {
            Repository repository = new Repository();
            var task = repository.GetSpecificTask(taskId);
            var taskDto = new TaskModel()
            {
                TaskId = task.Task_ID,
                Task = task.Task,
                StartDate = task.Start_Date.GetValueOrDefault().ToString(),
                EndDate = task.End_Date.ToString(),
                Priority = task.Priority.GetValueOrDefault(),
                ParentTask = task.ParentTask_Table != null ? task.ParentTask_Table.Parent_Task : "",
                ParentTaskId = task.ParentTask_Table!=null ? task.ParentTask_Table.Parent_ID: 0
            };
            return taskDto;
        }
        



        public void EndTask(TaskModel task)
        {
            Repository repository = new Repository();
            repository.EndTask(task.TaskId);

        }

        public void UpdateTask(TaskModel task)
        {
            Repository rep = new Repository();
            rep.DeleteTask(task.TaskId);
            AddTask(task);
        }

        public bool IsTaskExist(string taskName)
        {
            Repository rep = new Repository();
            return rep.IsTaskExists(taskName);
        }
    }
}
