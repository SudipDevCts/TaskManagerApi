using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBench.Util;
using NBench;
using System.Diagnostics.CodeAnalysis;

namespace TaskMaster.Tests
{
    [ExcludeFromCodeCoverage]
    public class TaskManager
    {
        private Counter _counter;
        private Controllers.TaskManagerController controller;
        private BusinessLayer.Models.TaskModel task;
        private int taskId;
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            _counter = context.GetCounter("TestCounter");
            controller = new TaskMaster.Controllers.TaskManagerController();
             
            task = new BusinessLayer.Models.TaskModel() { Task = "Test1", ParentTask = "Test2", Priority = 10, StartDate = System.DateTime.Now.ToString(), EndDate = System.DateTime.Now.AddDays(7).ToString() };

            taskId = new BusinessLayer.TaskManager().GetTasks().FirstOrDefault().TaskId;
        }
        [PerfBenchmark(Description = "Add task through put test.",
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1200, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void AddTask()
        {
            // Act on Test  
            controller.AddTask(task);
            _counter.Increment();
        }

        [PerfBenchmark(Description = "Get Specufic task.",
        NumberOfIterations = 500, RunMode = RunMode.Throughput,
        RunTimeMilliseconds = 1200, TestMode = TestMode.Measurement)]
        [CounterMeasurement("TestCounter")]
        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.ThirtyTwoKb)]
        [GcTotalAssertion(GcMetric.TotalCollections, GcGeneration.Gen2, MustBe.ExactlyEqualTo, 0.0d)]
        public void GetSpecificTask()
        {
            // Act on Test  

            controller.Task(taskId);
            _counter.Increment();
        }





        [PerfCleanup]
        public void Cleanup()
        {
            // does nothing
        }
    }
}
