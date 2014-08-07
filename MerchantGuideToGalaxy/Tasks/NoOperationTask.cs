namespace MerchantGuideToGalaxy.Tasks
{
    public class NoOperationTask : ITask
    {
        public bool CanRun(string line)
        {
            // This task can run on any line.
            // If it is included in the list of tasks it should be the last task in the list.
            return true;
        }


        public void Run(string line)
        {
        }
    }
}
