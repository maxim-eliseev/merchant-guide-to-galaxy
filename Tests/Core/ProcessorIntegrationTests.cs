﻿namespace MerchantGuideToGalaxy.Tests.Core
{
    using System.Linq;

    using MerchantGuideToGalaxy.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProcessorIntegrationTests
    {
        [TestMethod]
        public void Given_empty_input_when_Run_is_called_should_write_warning_to_output()
        {
            // Arrange

            // Act
            var results = new Processor().Process(new string[0] { });

            // Assert
            Assert.IsTrue(results.Single().Contains("empty"));
        }

        [TestMethod]
        public void Given_correct_input_without_question_when_Run_is_called_should_warning_to_output()
        {
            // Arrange
            // Act
            var results = new Processor().Process(new[] { "glob is I" });

            // Assert
            Assert.IsTrue(results.Any(line => line.Contains("questions")));
        }

        [TestMethod]
        public void Given_correct_number_data_and_number_question_when_Run_is_called_should_return_correct_answer()
        {
            // Arrange
            // Act
            var results = new Processor().Process(new[]
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
            var results = new Processor().Process(new[]
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
            var results = new Processor().Process(new[]
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
            var results = new Processor().Process(new[]
                                                      {
                                                          "glob is I",
                                                          "how many Credits is glob glob UNKNOWN ?"                                                          
                                                      });

            // Assert
            Assert.IsTrue(results.Count() == 2);
        }     
    }
}