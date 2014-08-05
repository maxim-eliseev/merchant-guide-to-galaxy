﻿namespace MerchantGuideToGalaxy.Tests.Converters
{
    using System;

    using MerchantGuideToGalaxy.Converters;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AlienToRomanConvertorTests
    {
        private Context context;

        [TestInitialize]
        public void Init()
        {
            context = new Context();
        }

        [TestMethod]
        public void Given_empty_string_and_no_added_symbols_should_return_empty_string()
        {
            // Assert
            var convertor = new AlienToRomanConvertor(context);

            // Arrange
            string romanNumber = convertor.Convert(new string[0]);

            // Act
            Assert.AreEqual(string.Empty, romanNumber);
        }

        [TestMethod]
        public void Given_empty_string_and_some_added_symbols_should_return_empty_string()
        {
            // Assert
            var convertor = new AlienToRomanConvertor(context);
            convertor.AddAlienSymbol("dfg","I");

            // Arrange
            string romanNumber = convertor.Convert(new string[0]);

            // Act
            Assert.AreEqual(string.Empty, romanNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Given_unknown_alien_symbols_should_throw_exception()
        {
            // Assert
            var convertor = new AlienToRomanConvertor(context);
            convertor.AddAlienSymbol("dfg", "I");

            // Arrange
            convertor.Convert(new string[]{"hdgdshd"});
        }

        [TestMethod]
        public void Given_known_alien_symbols_should_convert_correctly()
        {
            // Assert
            var convertor = new AlienToRomanConvertor(context);
            convertor.AddAlienSymbol("hds", "Z");
            convertor.AddAlienSymbol("dfg", "Q");
            
            // Arrange
            var romanNumber = convertor.Convert(new string[] { "dfg","hds" });

            // Assert
            Assert.AreEqual("QZ", romanNumber);
        }


    }
}