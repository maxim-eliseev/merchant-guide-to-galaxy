namespace MerchantGuideToGalaxy.Tests.Tasks
{
    using System;
    using System.Linq;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MineralPriceQuestionAnswererTaskTests
    {
        private MineralPriceQuestionAnswererTask task;

        private Context context;

        [TestInitialize]
        public void Init()
        {
            this.context = new Context();

            var convertor = new AlienToArabicConvertor(
                                            new AlienToRomanConvertor(context),
                                            new RomanToArabicConvertor()
            );

            this.task = new MineralPriceQuestionAnswererTask(this.context, convertor);
        }


        [TestMethod]
        public void Given_correct_line_when_Run_is_called_should_write_correct_response_to_output()
        {
            // Arrange
            context.AlienToRomanNumberMap.Add("glob", "I");
            context.AlienToRomanNumberMap.Add("prok", "V");
            context.MineralPricesPerUnit.Add("Silver", 17);

            var line = "how many Credits is glob prok Silver ?";                        

            // Act
            task.Run(line);

            // Assert
            var expectedAnswer = "glob prok Silver is 68 Credits"; // glob is I. prok is V. glob prok is IV = 4. 17*2 = 68
            var actualAnswer = context.Output.Single();
            Assert.AreEqual(expectedAnswer, actualAnswer);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Given_line_with_unknown_mineral_name_when_Run_is_called_should_throw_error()
        {
            // Arrange
            context.AlienToRomanNumberMap.Add("glob", "I");
            context.AlienToRomanNumberMap.Add("prok", "V");

            var line = "how many Credits is glob prok UNKNOWN ?";

            // Act
            task.Run(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Given_line_with_first_unknown_alien_symbol_when_Run_is_called_should_throw_error()
        {
            // Arrange
            context.AlienToRomanNumberMap.Add("glob", "I");

            var line = "how many Credits is UNKNOWN prok Silver ?";

            // Act
            task.Run(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Given_line_with_last_unknown_alien_symbol_when_Run_is_called_should_throw_error()
        {
            // Arrange
            context.AlienToRomanNumberMap.Add("glob", "I");

            var line = "how many Credits is prok UNKNOWN Silver ?";

            // Act
            task.Run(line);
        }      
    }
}
