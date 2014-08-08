namespace MerchantGuideToGalaxy.Tasks
{
    using System;
    using System.Collections.Generic;

    public class TaskFactory
    {
        private readonly IEnumerable<ITask> tasks;

        // The list of tasks is configured in ProcessorConfigurationModule
        public TaskFactory(IEnumerable<ITask> tasks)
        {
            this.tasks = tasks;
        }

        public ITask CreateTask(string inputLine)
        {
            foreach (var task in tasks)
            {
                if (task.CanRun(inputLine))
                {
                    return task;
                }
            }

            throw new InvalidOperationException("No suitable task found. Input line:" + inputLine);
        }        
    }
}