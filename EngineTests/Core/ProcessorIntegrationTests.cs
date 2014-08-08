namespace MerchantGuideToGalaxy.Tests.Core
{
    using System.Linq;

    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.DependencyInjection;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Ninject;

    [TestClass]
    public class ProcessorIntegrationTests
    {
        private Processor processor;

        [TestInitialize]
        public void Init()
        {
            IKernel kernel = new StandardKernel(new ProcessorConfigurationModule());
            this.processor = kernel.Get<Processor>();                        
        }

        [TestMethod]
        public void Given_empty_input_when_Run_is_called_should_write_warning_to_output()
        {
            // Arrange

            // Act
            var results = this.processor.Process(new string[0] { });

            // Assert
            Assert.IsTrue(results.Single().Contains("empty"));
        }
        
        [TestMethod]
        public void Given_correct_number_data_and_number_question_when_Run_is_called_should_return_correct_answer()
        {
            // Arrange
            // Act
            var results = this.processor.Process(new[]
                                                      {
                                                          "glob is I",
                                                          "prok is V",
                                                          "pish is X",
                                                          "tegj is L",
                                                          "how much is pish tegj glob glob ?"
                                                      });

            // Assert
            Assert.IsTrue(results.Single() == "pish tegj glob glob is 42");
        }

        [TestMethod]
        public void Given_correct_number_data_and_silver_price_and_price_question_when_Run_is_called_should_return_correct_answer()
        {
            // Arrange
            // Act
            var results = this.processor.Process(new[]
                                                      {
                                                          "glob is I",
                                                          "prok is V",
                                                          "glob glob Silver is 34 Credits",
                                                          "how many Credits is glob prok Silver ?"                                                          
                                                      });

            // Assert
            Assert.IsTrue(results.Single() == "glob prok Silver is 68 Credits");
        }     
   
        [TestMethod]
        public void Given_incorrect_question_when_Run_is_called_should_return_error_message()
        {
            // Arrange
            // Act
            var results = this.processor.Process(new[]
                                                      {
                                                          "how much wood could a woodchuck chuck if a woodchuck could chuck wood ?"
                                                      });

            // Assert
            Assert.IsTrue(results.Single() == "I have no idea what you are talking about");
        }

        [TestMethod]
        public void Given_malformed_question_when_Run_is_called_should_return_error_message_and_exception_details()
        {
            // Arrange
            // Act
            var results = this.processor.Process(new[]
                                                      {
                                                          "glob is I",
                                                          "how many Credits is glob glob UNKNOWN ?"                                                          
                                                      });

            // Assert
            Assert.IsTrue(results.Count() == 2);
        }

    }
}
