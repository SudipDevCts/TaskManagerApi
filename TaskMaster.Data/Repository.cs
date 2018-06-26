using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMaster.Data;

namespace TaskMaster.Data
{
    public class Repository
    {
        public void AddTask(Task_Table task)
        {
            using (var db = new TaskManagerEntities())
            {

                db.Task_Table.Add(task);

                db.SaveChanges();


            }
        }

        public IList<Task_Table> GetTasks()
        {
            using (var db = new TaskManagerEntities())
            {
                return db.Task_Table.Include("ParentTask_Table").ToList();
            }
        }



        public Task_Table GetSpecificTask(int taskId)
        {
            using (var db = new TaskManagerEntities())
            {
                return db.Task_Table.Include("ParentTask_Table").FirstOrDefault(x => x.Task_ID == taskId);
            }
        }

        public void EndTask(int taskId)
        {
            using (var db = new TaskManagerEntities())
            {
                var task = db.Task_Table.FirstOrDefault(x => x.Task_ID == taskId);
                task.End_Date = System.DateTime.Now;
                db.SaveChanges();
            }
        }

        public void DeleteTask(int taskId)
        {
            using (var db = new TaskManagerEntities())
            {
                var task = db.Task_Table.FirstOrDefault(x => x.Task_ID == taskId);
                if (task != null)
                {
                    db.Task_Table.Remove(task);
                    db.SaveChanges();
                }
            }


        }

        public bool IsTaskExists(string taskName)
        {
            using (var db = new TaskManagerEntities())
            {
                var task = db.Task_Table.FirstOrDefault(x => x.Task == taskName);
                if (task != null)
                    return true;
                else
                    return false;
            }
        }

    }
}
