namespace ConsoleApplication.DependencyInjection
{
    using ConsoleApplication.Wrappers;

    public class ConsoleConfigurationModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IUiController>().To<UiController>();
            Bind<IConsoleWrapper>().To<ConsoleWrapper>();
            Bind<IFileWrapper>().To<FileWrapper>();
        }
    }
}