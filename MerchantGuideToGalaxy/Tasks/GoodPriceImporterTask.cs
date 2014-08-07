﻿namespace MerchantGuideToGalaxy.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Utils;

    public class GoodPriceImporterTask : ITask
    {
        private const string MatchingPattern = @"(.+) is (\w+) Credits";
        // (.+) is one or more any characters (including whitespace)
        // (\w+) is one or more any characters (no whitespaces - just one word)

        private readonly Context context;

        public GoodPriceImporterTask(Context context)
        {
            this.context = context;
        }

        public bool CanRun(string inputLine)
        {
            if (inputLine.IsQuestion())
            {
                return false;
            }

            return inputLine.IsMatch(MatchingPattern);
        }

        ////  glob glob Silver is 34 Credits
        public void Run(string inputLine)
        {
            var extractedData = inputLine.MatchTwoGroups(MatchingPattern);
            if (extractedData == null)
            {
                throw new ArgumentException("Input line cannot be parsed by GoodPriceImporterTask:" + inputLine);
            }

            var goodsNameAndAmountAsString = extractedData.Item1;
            string totalPriceAsString = extractedData.Item2;

            decimal totalPriceAsNumber;
            if (!decimal.TryParse(totalPriceAsString, out totalPriceAsNumber))
            {
                throw new ArgumentException("Price is not a number:" + totalPriceAsNumber);
            }

            var goodsNameAndAmount = LineParsingUtility.Split(goodsNameAndAmountAsString); // "glob glob Silver"

            IEnumerable<string> goodsAmountAsAlienNumber = goodsNameAndAmount.WithoutLast(); // "glob glob"
            string goodsName = goodsNameAndAmount.Last(); // "Silver"

            var goodsAmount = new AlienToArabicConvertor(context).Convert(goodsAmountAsAlienNumber);

            var pricePerUnit = totalPriceAsNumber / goodsAmount;

            context.GoodsPricesPerUnit[goodsName] = pricePerUnit;
        }
    }
}
