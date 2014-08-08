namespace MerchantGuideToGalaxy.Tasks
{

    /// <summary>
    /// Processes empty lines
    /// </summary>
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
