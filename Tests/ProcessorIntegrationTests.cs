namespace MerchantGuideToGalaxy.Tests
{
    using System.Linq;

    using MerchantGuideToGalaxy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProcessorIntegrationTests
    {
        [TestMethod]
        public void Given_empty_input_when_Run_is_called_should_write_warning_to_output()
        {
            // Arrange

            // Act
            var results = new Processor(new string[0] { }).Process();

            // Assert
            Assert.IsTrue(results.Single().Contains("empty"));
        }

        [TestMethod]
        public void Given_correct_input_without_question_when_Run_is_called_should_warning_to_output()
        {
            // Arrange
            // Act
            var results = new Processor(new[] { "glob is I" }).Process();

            // Assert
            Assert.IsTrue(results.Any(line => line.Contains("questions")));
        }

        [TestMethod]
        public void Given_correct_number_data_and_number_question_when_Run_is_called_should_return_correct_answer()
        {
            // Arrange
            // Act
            var results = new Processor(new[]
                                                      {
                                                          "glob is I",
                                                          "prok is V",
                                                          "pish is X",
                                                          "tegj is L",
                                                          "how much is pish tegj glob glob ?"
                                                      }).Process();

            // Assert
            Assert.IsTrue(results.Single() == "pish tegj glob glob is 42");
        }

        [TestMethod]
        public void Given_correct_number_data_and_silver_price_and_price_question_when_Run_is_called_should_return_correct_answer()
        {
            // Arrange
            // Act
            var results = new Processor(new[]
                                                      {
                                                          "glob is I",
                                                          "prok is V",
                                                          "glob glob Silver is 34 Credits",
                                                          "how many Credits is glob prok Silver ?"                                                          
                                                      }).Process();

            // Assert
            Assert.IsTrue(results.Single() == "glob prok Silver is 68 Credits");
        }        
    }
}
