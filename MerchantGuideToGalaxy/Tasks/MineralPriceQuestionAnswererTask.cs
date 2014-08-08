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
    /// Processes lines which ask questions to calculate price of a specific amount of a mineral (set by an alien number)
    /// </summary>
    /// <example>
    ///  how many Credits is glob prok Silver ?
    /// </example>
    public class MineralPriceQuestionAnswererTask : ITask
    {
        private const string MatchingPattern = @"how many Credits is ([\w\s]+) ?";
        //// [\w\s]+ is one or more word characters OR whitespaces. This is done to exclude "?" from matching.
        //// () indicates a capturing group

        private readonly Context context;

        private readonly AlienToArabicConvertor alienToArabicConvertor;

        public MineralPriceQuestionAnswererTask(Context context, AlienToArabicConvertor alienToArabicConvertor)
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
            var mineralNameAndAmountAsString = inputLine.MatchOneGroup(MatchingPattern);
            var mineralNameAndAmount = LineParsingUtility.Split(mineralNameAndAmountAsString);

            var mineralName = mineralNameAndAmount.Last();
            var alienNumber = mineralNameAndAmount.WithoutLast();

            if (!context.MineralPricesPerUnit.ContainsKey(mineralName))
            {
                throw new ArgumentException("Unknown mineral name:" + mineralName);
            }
            decimal priceOfUnit = context.MineralPricesPerUnit[mineralName];

            var arabicNumber = alienToArabicConvertor.Convert(alienNumber);
            int numberOfUnits = arabicNumber;

            decimal totalPrice = priceOfUnit * numberOfUnits;

            var alienNumberAsString = string.Join(" ", alienNumber);
            var response = string.Format("{0} {1} is {2} Credits", alienNumberAsString, mineralName, totalPrice);
            context.Output.Add(response);
        }
    }
}
