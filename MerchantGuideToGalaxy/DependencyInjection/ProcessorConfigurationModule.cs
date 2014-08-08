namespace MerchantGuideToGalaxy.DependencyInjection
{
    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.Tasks;

    public class ProcessorConfigurationModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            this.Bind<IProcessor>().To<Processor>();
            this.Bind<IContext>().To<Context>().InSingletonScope(); // Context is sinlgeton because we want to reuse same instance in all the classes

            this.BindTasks();
        }

        private void BindTasks()
        {
            // https://github.com/ninject/ninject/wiki/Multi-injection
            this.Bind<ITask>().To<EmptyLineProcessingTask>();

            this.Bind<ITask>().To<AlienNumberImporterTask>();
            this.Bind<ITask>().To<MineralPriceImporterTask>();

            this.Bind<ITask>().To<AlienNumberQuestionAnswererTask>();
            this.Bind<ITask>().To<MineralPriceQuestionAnswererTask>();

            this.Bind<ITask>().To<ErrorMessageTask>();

            // Note that order of tasks is important. 
            // Initial version of the task list looked as follows:
            // tasks = new List<ITask>()
            //            {
            //                new EmptyLineProcessingTask(),      
            //                new AlienNumberImporterTask(context),
            //                new MineralPriceImporterTask(context),
            //                new AlienNumberQuestionAnswererTask(context),
            //                new MineralPriceQuestionAnswererTask(context),
            //                new ErrorMessageTask(context) // This task runs on any line. It must be the last in task list.
            ////          };
        }
    }
}