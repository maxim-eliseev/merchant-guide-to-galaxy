namespace MerchantGuideToGalaxy.Tests.Converters
{
    using System;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AlienToArabicConvertorTests
    {
        private Context context;

        private AlienToArabicConvertor convertor;

        [TestInitialize]
        public void Init()
        {
            context = new Context();

            convertor = new AlienToArabicConvertor(
                                            new AlienToRomanConvertor(context),
                                            new RomanToArabicConvertor()
            );
        }

        [TestMethod]
        public void Given_known_alien_symbols_should_convert_correctly()
        {
            // Arrange
            this.AddAlienSymbolToContext("one", "I");
            this.AddAlienSymbolToContext("four", "V");

            // Act
            var arabicNumber = convertor.Convert(new string[] { "one", "four" }); // IV = 4

            // Assert
            Assert.AreEqual(4, arabicNumber);
        }

        public void AddAlienSymbolToContext(string alienSymbol, string romanSymbol)
        {
            this.context.AlienToRomanNumberMap[alienSymbol] = romanSymbol;
        }
    }
}
