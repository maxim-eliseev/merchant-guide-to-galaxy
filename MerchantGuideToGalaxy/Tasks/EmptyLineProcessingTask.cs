namespace MerchantGuideToGalaxy.Tasks
{
    public class EmptyLineProcessingTask : ITask
    {
        public bool CanRun(string line)
        {
            return string.IsNullOrWhiteSpace(line);
        }

        public void Run(string line)
        {
        }
    }
}
