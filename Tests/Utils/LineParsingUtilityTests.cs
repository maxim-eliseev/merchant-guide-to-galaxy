namespace MerchantGuideToGalaxy.Tests
{
    using System;

    using MerchantGuideToGalaxy.Utils;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LineParsingUtilityTests
    {      
        [TestMethod]
        public void Given_alien_number_import_string_when_MatchTwoGroups_called_should_parse_correctly()
        {
            // Arrange
            string text = "glob is I";
            string pattern = @"(\w+) is (\w+)";  // Version for variable number of whitespaces: @"(\w+)\s+(?:is)\s+(\w+)";

            // Act
            var results = LineParsingUtility.MatchTwoGroups(text, pattern);

            // Assert
            Assert.IsNotNull(results);

            var alienSymbol = results.Item1;
            Assert.AreEqual("glob", alienSymbol);

            var romanSymbol = results.Item2;
            Assert.AreEqual("I", romanSymbol);
        }

        [TestMethod]
        public void Given_alien_number_import_string__with_extra_spaces_when_MatchTwoGroups_called_should_parse_correctly()
        {
            // Arrange
            string text = "glob  is  I";
            string pattern = @"(\w+)\s+(?:is)\s+(\w+)";

            // Act
            var results = LineParsingUtility.MatchTwoGroups(text, pattern);

            // Assert
            Assert.IsNotNull(results);

            var alienSymbol = results.Item1;
            Assert.AreEqual("glob", alienSymbol);

            var romanSymbol = results.Item2;
            Assert.AreEqual("I", romanSymbol);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Given_alien_number_import_string_twice_when_MatchTwoGroups_called_should_throw_error()
        {
            // Arrange
            string text = "glob is I glob is I";
            string pattern = @"(\w+) is (\w+)";  // Version for variable number of whitespaces: @"(\w+)\s+(?:is)\s+(\w+)";

            // Act
            LineParsingUtility.MatchTwoGroups(text, pattern);           
        }

        [TestMethod]
        public void Given_wrong_string_when_MatchTwoGroups_called_should_return_null()
        {
            // Arrange
            string text = "glob I is";
            string pattern = @"(\w+) is (\w+)";  // Version for variable number of whitespaces: @"(\w+)\s+(?:is)\s+(\w+)";

            // Act
            var results = LineParsingUtility.MatchTwoGroups(text, pattern);

            // Assert
            Assert.IsNull(results);
        }

        [TestMethod]
        public void Given_good_price_import_string_when_MatchTwoGroups_called_should_parse_correctly()
        {
            // Arrange
            string text = "glob glob Silver is 34 Credits";
            string pattern = @"(.+) is (.+) Credits"; // "." matches any character except whitespace. "+" indicates one or more matches.
            

            // Act
            var results = LineParsingUtility.MatchTwoGroups(text, pattern);

            // Assert
            Assert.IsNotNull(results);

            var alienNumberAndGoodsName = results.Item1;
            Assert.AreEqual("glob glob Silver", alienNumberAndGoodsName);

            var goodsPrice = results.Item2;
            Assert.AreEqual("34", goodsPrice);
        }


    }
}
