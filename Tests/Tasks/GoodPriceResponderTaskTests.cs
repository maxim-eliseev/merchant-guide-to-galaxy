namespace MerchantGuideToGalaxy.Tests.Tasks
{
    using System;
    using System.Linq;

    using MerchantGuideToGalaxy;
    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GoodPriceResponderTaskTests
    {
        [TestMethod]
        public void Given_correct_line_when_Run_is_called_should_write_correct_response_to_output()
        {
            // Arrange
            var context = new Context();
            context.AlienToRomanNumberMap.Add("glob", "I");
            context.AlienToRomanNumberMap.Add("prok", "V");
            context.GoodsPricesPerUnit.Add("Silver", 17);

            var line = "how many Credits is glob prok Silver ?";                        
            var task = new GoodPriceResponderTask(context);

            // Act
            task.Run(line);

            // Assert
            var expectedAnswer = "glob prok Silver is 68 Credits"; // glob is I. prok is V. glob prok is IV = 4. 17*2 = 68
            var actualAnswer = context.Output.Single();
            Assert.AreEqual(expectedAnswer, actualAnswer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Given_line_with_unknown_goods_name_when_Run_is_called_should_throw_error()
        {
            // Arrange
            var context = new Context();
            context.AlienToRomanNumberMap.Add("glob", "I");
            context.AlienToRomanNumberMap.Add("prok", "V");

            var line = "how many Credits is glob prok UNKNOWN ?";

            var task = new GoodPriceResponderTask(context);

            // Act
            task.Run(line);
        }
        
        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Given_line_with_first_unknown_alien_symbol_when_Run_is_called_should_throw_error()
        //{
        //    // Arrange
        //    var context = new Context();
        //    context.AlienToRomanNumberMap.Add("glob", "I");

        //    var line = "UNKNOWN glob Silver is 34 Credits";

        //    var task = new GoodPriceImporterTask(context);

        //    // Act
        //    task.Run(line);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ArgumentException))]
        //public void Given_line_with_last_unknown_alien_symbol_when_Run_is_called_should_throw_error()
        //{
        //    // Arrange
        //    var context = new Context();
        //    context.AlienToRomanNumberMap.Add("glob", "I");

        //    var line = "glob UNKNOWN Silver is 34 Credits";

        //    var task = new GoodPriceImporterTask(context);

        //    // Act
        //    task.Run(line);
        // }

    }
}
