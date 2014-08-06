namespace MerchantGuideToGalaxy.Tests.Converters
{
    using System;

    using MerchantGuideToGalaxy.Converters;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AlienToArabicConvertorTests
    {
        private Context context;

        [TestInitialize]
        public void Init()
        {
            context = new Context();
        }

        [TestMethod]
        public void Given_known_alien_symbols_should_convert_correctly()
        {
            // Arrange
            var convertor = new AlienToArabicConvertor(context);
            this.AddAlienSymbolToContext("one", "I");
            this.AddAlienSymbolToContext("four", "V");

            // Act
            var arabicNumber = convertor.Convert(new string[] { "one" , "four" }); // IV = 4

            // Assert
            Assert.AreEqual(4, arabicNumber);
        }

        public void AddAlienSymbolToContext(string alienSymbol, string romanSymbol)
        {
            this.context.AlienToRomanNumberMap[alienSymbol] = romanSymbol;
        }
    }
}
