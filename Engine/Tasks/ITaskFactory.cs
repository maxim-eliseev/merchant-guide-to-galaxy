namespace MerchantGuideToGalaxy.Tasks
{
    public interface ITaskFactory
    {
        ITask CreateTask(string inputLine);
    }
}