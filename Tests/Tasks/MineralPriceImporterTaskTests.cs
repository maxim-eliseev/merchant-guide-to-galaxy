namespace MerchantGuideToGalaxy.Tests.Tasks
{
    using System;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.Tasks;
    using MerchantGuideToGalaxy.Utils;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class MineralPriceImporterTaskTests
    {
        private MineralPriceImporterTask task;

        private Context context;

        [TestInitialize]
        public void Init()
        {
            this.context = new Context();

            var convertor = new AlienToArabicConvertor(
                                                        new AlienToRomanConvertor(context), 
                                                        new RomanToArabicConvertor()
                );

            this.task = new MineralPriceImporterTask(this.context, convertor);
        }

        [TestMethod]
        public void Given_correct_line_when_Run_is_called_should_import_mineral_price_to_context()
        {
            // Arrange            
            context.AlienToRomanNumberMap.Add("glob", "I");

            var line = "glob glob Silver is 34 Credits";
            var mineralName = "Silver";
            var expectedPricePerUnit = 17; // glob is I. glob glob is II = 2. 34/2 = 17
            
            // Act
            task.Run(line);

            // Assert
            var actualPricePerUnit = context.MineralPricesPerUnit[mineralName];
            Assert.AreEqual(expectedPricePerUnit, actualPricePerUnit);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void Given_line_without_Credits_as_last_word_when_Run_is_called_should_throw_error()
        {
            // Act
            task.Run("glob glob Silver is 34 NOT_CREDITS");
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void Given_line_without_IS_when_Run_is_called_should_throw_error()
        {
            // Act
            task.Run("glob glob Silver NOT_IS 34 Credits");
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void Given_line_with_non_numerical_price_when_Run_is_called_should_throw_error()
        {
            // Act
            task.Run("glob glob Silver is NOT_A_NUMBER Credits");
        }
        
        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void Given_line_with_first_unknown_alien_symbol_when_Run_is_called_should_throw_error()
        {
            // Arrange
            context.AlienToRomanNumberMap.Add("glob", "I");

            var line = "UNKNOWN glob Silver is 34 Credits";

            // Act
            task.Run(line);
        }

        [TestMethod]
        [ExpectedException(typeof(ParsingException))]
        public void Given_line_with_last_unknown_alien_symbol_when_Run_is_called_should_throw_error()
        {
            // Arrange
            context.AlienToRomanNumberMap.Add("glob", "I");

            var line = "glob UNKNOWN Silver is 34 Credits";

            // Act
            task.Run(line);
        }

    }
}
