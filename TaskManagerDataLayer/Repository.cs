using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDataLayer
{
    public class Repository
    {
        public void AddTask(Task task)
        {
            using (var db = new TaskManagerEntities()) {
                
                db.Tasks.Add(task);
                db.SaveChanges();
            } 
        }

    }
}
