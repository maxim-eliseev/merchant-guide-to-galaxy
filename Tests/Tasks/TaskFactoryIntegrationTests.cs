namespace MerchantGuideToGalaxy.Tests.Tasks
{
    using System;

    using MerchantGuideToGalaxy.DependencyInjection;
    using MerchantGuideToGalaxy.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    [TestClass]
    public class TaskFactoryIntegrationTests
    {
        private TaskFactory taskFactory;

        [TestInitialize]
        public void Init()
        {
            IKernel kernel = new StandardKernel(new NinjectConfigurationModule());
            this.taskFactory = kernel.Get<TaskFactory>();
        }

        [TestMethod]
        public void Given_alien_number_import_line_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("prok is V", typeof(AlienNumberImporterTask));
        }

        [TestMethod]
        public void Given_alien_number_conversion_question_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("how much is pish tegj glob glob ?", typeof(AlienNumberQuestionAnswererTask));
        }

        [TestMethod]
        public void Given_mineral_price_import_line_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("glob glob Silver is 34 Credits", typeof(MineralPriceImporterTask));
        }

        [TestMethod]
        public void Given_mineral_price_question_line_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("how many Credits is glob prok Silver ?", typeof(MineralPriceQuestionAnswererTask));
        }

        [TestMethod]
        public void Given_whitespace_line_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("    ", typeof(EmptyLineProcessingTask));
        }

        private void AssertTaskType(string line, Type expectedType)
        {
            // Arrange

            // Act
            var task = this.taskFactory.CreateTask(line);

            // Assert            
            Assert.IsInstanceOfType(task, expectedType);
        }
    }
}
