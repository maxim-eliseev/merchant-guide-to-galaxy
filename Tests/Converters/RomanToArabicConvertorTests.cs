namespace MerchantGuideToGalaxy.Tests.Converters
{
    using MerchantGuideToGalaxy.Converters;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RomanToArabicConvertorTests
    {
        [TestMethod]
        public void Given_empty_string_should_return_0()
        {
            AssertResult(string.Empty,0);
        }

        [TestMethod]
        public void Given_number_with_all_symbols_once_should_return_correct_result()
        {
            AssertResult("MDCLXVI", 1666);
        }

        [TestMethod]
        public void Given_number_with_all_symbols_twice_should_return_correct_result()
        {
            // D L and V cannot be repeated - http://www.codesandscripts.com/2014/06/merchant-guide-to-galaxy.html#more
            AssertResult("MMDCCLXXVII", 2777);
        }

        [TestMethod]
        public void Given_number_with_substractions_should_return_correct_result()
        {
            AssertResult("MCMXCIX", 1999);
        }

        private static void AssertResult(string roman, int arabic)
        {
            var result = new RomanToArabicConvertor().Convert(roman);
            Assert.AreEqual(arabic, result);
        }
    }
}
