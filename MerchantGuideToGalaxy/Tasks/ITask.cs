namespace MerchantGuideToGalaxy.Tasks
{
    using System.Runtime.InteropServices;

    public interface ITask
    {
        bool CanRun(string line);
        void Run(string line);
    }
}
