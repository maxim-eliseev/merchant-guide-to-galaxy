namespace ConsoleApplication
{
    using ConsoleApplication.DependencyInjection;

    using MerchantGuideToGalaxy.DependencyInjection;

    using Ninject;

    public class Program
    {
        private const string InputFileName = "input.txt";

        public static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(
                    new ConsoleConfigurationModule(),
                    new ProcessorConfigurationModule()
                );

            var uiController = kernel.Get<IUiController>();
            uiController.Run(InputFileName);
        }
    }
}
