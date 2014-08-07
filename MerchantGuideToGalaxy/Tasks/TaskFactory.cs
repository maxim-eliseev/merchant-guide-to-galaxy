namespace MerchantGuideToGalaxy.Tasks
{
    using System.Collections.Generic;

    using MerchantGuideToGalaxy.Core;

    public class TaskFactory
    {
        private readonly Context context;

        private readonly IEnumerable<ITask> tasks;

        public TaskFactory(Context context)
        {
            this.context = context;

            tasks = new List<ITask>()
                        {
                            new EmptyLineProcessingTask(),      
                            new AlienNumberImporterTask(context),
                            new GoodPriceImporterTask(context),
                            new AlienNumberConversionResponderTask(context),
                            new GoodPriceResponderTask(context)
                        };
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

            return new ErrorMessageTask(context);
        }        
    }
}