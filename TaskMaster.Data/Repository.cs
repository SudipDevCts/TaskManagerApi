using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Data;

namespace TaskMaster.Data
{
    [ExcludeFromCodeCoverage]
    public class Repository: IRepository
    {
        private TaskManagerEntities _db;
        public Repository()
        {
            TaskManagerEntities db = new TaskManagerEntities();
            _db = db;
        }
        public Repository(TaskManagerEntities db)
        {
            _db = db;
        }
        public void AddTask(Task_Table task)
        {
            
                _db.Task_Table.Add(task);

                _db.SaveChanges();


        }

        public IList<Task_Table> GetTasks()
        {
                return _db.Task_Table.Include("ParentTask_Table").ToList();
        }



        public Task_Table GetSpecificTask(int taskId)
        {
                return _db.Task_Table.Include("ParentTask_Table").FirstOrDefault(x => x.Task_ID == taskId);
        }

        public void EndTask(int taskId)
        {
                var task = _db.Task_Table.FirstOrDefault(x => x.Task_ID == taskId);
                task.End_Date = System.DateTime.Now;
                _db.SaveChanges();
        }

        public void DeleteTask(int taskId)
        {
                var task = _db.Task_Table.FirstOrDefault(x => x.Task_ID == taskId);
                if (task != null)
                {
                    _db.Task_Table.Remove(task);
                    _db.SaveChanges();
                }


        }   
            
        public bool IsTaskExists(string taskName)
        {
                var task = _db.Task_Table.FirstOrDefault(x => x.Task == taskName);
                if (task != null)
                    return true;
                else
                    return false;
        }

    }
}
