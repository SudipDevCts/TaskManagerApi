using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMaster.Data
{
    public interface IRepository
    {
        void AddTask(Task_Table task);
        IList<Task_Table> GetTasks();
        Task_Table GetSpecificTask(int taskId);
        void EndTask(int taskId);
        void DeleteTask(int taskId);
        bool IsTaskExists(string taskName);
    }
}
