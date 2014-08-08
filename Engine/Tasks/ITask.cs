namespace MerchantGuideToGalaxy.Tasks
{
    public interface ITask
    {
        bool CanRun(string line);
        void Run(string line);
    }
}
