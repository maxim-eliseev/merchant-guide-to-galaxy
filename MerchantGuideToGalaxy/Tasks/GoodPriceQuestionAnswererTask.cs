namespace MerchantGuideToGalaxy.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.Utils;

    /// <summary>
    /// Processes lines which ask questions to calculate price of a specific amount of goods (set by an alien number)
    /// </summary>
    /// <example>
    ///  how many Credits is glob prok Silver ?
    /// </example>
    public class GoodPriceQuestionAnswererTask : ITask
    {
        private const string MatchingPattern = @"how many Credits is ([\w\s]+) ?";
        //// [\w\s]+ is one or more word characters OR whitespaces. This is done to exclude "?" from matching.
        //// () indicates a capturing group

        private readonly Context context;

        private readonly AlienToArabicConvertor alienToArabicConvertor;

        public GoodPriceQuestionAnswererTask(Context context, AlienToArabicConvertor alienToArabicConvertor)
        {
            this.context = context;
            this.alienToArabicConvertor = alienToArabicConvertor;
        }

        public bool CanRun(string inputLine)
        {
            if (!inputLine.IsQuestion())
            {
                return false;
            }

            return inputLine.IsMatch(MatchingPattern);
        }

        public void Run(string inputLine)
        {
            var goodsNameAndAmountAsString = inputLine.MatchOneGroup(MatchingPattern);
            var goodsNameAndAmount = LineParsingUtility.Split(goodsNameAndAmountAsString);

            var goodsName = goodsNameAndAmount.Last();
            var alienNumber = goodsNameAndAmount.WithoutLast();

            if (!context.GoodsPricesPerUnit.ContainsKey(goodsName))
            {
                throw new ArgumentException("Unknown goods name:" + goodsName);
            }
            decimal priceOfUnit = context.GoodsPricesPerUnit[goodsName];

            var arabicNumber = alienToArabicConvertor.Convert(alienNumber);
            int numberOfUnits = arabicNumber;

            decimal totalPrice = priceOfUnit * numberOfUnits;

            var alienNumberAsString = string.Join(" ", alienNumber);
            var response = string.Format("{0} {1} is {2} Credits", alienNumberAsString, goodsName, totalPrice);
            context.Output.Add(response);
        }
    }
}
