namespace MerchantGuideToGalaxy.Tests.Tasks
{
    using System;

    using MerchantGuideToGalaxy;
    using MerchantGuideToGalaxy.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TaskFactoryTests
    {
        [TestMethod]
        public void Given_alien_number_import_line_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("prok is V", typeof(AlienNumberImporterTask));
        }

        [TestMethod]
        public void Given_alien_number_conversion_question_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("how much is pish tegj glob glob ?", typeof(AlienNumberConversionResponderTask));
        }

        [TestMethod]
        public void Given_good_price_import_line_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("glob glob Silver is 34 Credits", typeof(GoodPriceImporterTask));
        }

        [TestMethod]
        public void Given_good_price_question_line_when_CreateTask_is_called_should_create_appropriate_task()
        {
            AssertTaskType("how many Credits is glob prok Silver ?", typeof(GoodPriceResponderTask));
        }

        private static void AssertTaskType(string line, Type expectedType)
        {
            // Arrange
            var context = new Context();
            var taskFactory = new TaskFactory(context);

            // Act
            var task = taskFactory.CreateTask(line);

            // Assert            
            Assert.IsInstanceOfType(task, expectedType);
        }
    }
}
