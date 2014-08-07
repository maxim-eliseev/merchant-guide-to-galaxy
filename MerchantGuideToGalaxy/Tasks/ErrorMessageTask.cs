namespace MerchantGuideToGalaxy.Tasks
{
    using MerchantGuideToGalaxy.Core;

    /// <summary>
    /// Processes any line which is not processed by other tasks
    /// </summary>
    public class ErrorMessageTask : ITask
    {
        public const string GenericMessage = "I have no idea what you are talking about";

        private readonly Context context;

        public ErrorMessageTask(Context context)
        {
            this.context = context;
        }

        public bool CanRun(string line)
        {
            // This task can run on any line.
            // If it is included in the list of tasks it should be the last task in the list.
            return true;
        }

        public void Run(string line)
        {            
            context.Output.Add(GenericMessage);
        }
    }
}
