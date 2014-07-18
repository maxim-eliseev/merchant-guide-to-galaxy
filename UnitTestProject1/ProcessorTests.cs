//-----------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Abercrombie & Kent Ltd">
// ©2012-2014 Abercrombie & Kent Ltd. All rights reserved.
// * NOTICE:  All information contained herein is, and remains
// * the property of Abercrombie & Kent and its suppliers,
// * if any.  The intellectual and technical concepts contained
// * herein are proprietary to Abercrombie & Kent
// * and its suppliers and may be covered by U.S/U.K. and Foreign Patents,
// * patents in process, and are protected by trade secret or copyright law.
// * Dissemination of this information or reproduction of this material
// * is strictly forbidden unless prior written permission is obtained
// * from Abercrombie & Kent.
// </copyright>
//-----------------------------------------------------------------------

namespace UnitTestProject1
{
    using System.IO;
    using System.Linq;
    using System.Text;

    using MerchantGuideToGalaxy;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ProcessorTests
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
    }
}
