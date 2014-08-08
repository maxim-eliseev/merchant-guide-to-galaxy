namespace MerchantGuideToGalaxy.Tasks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.Utils;

    /// <summary>
    /// Processes lines which import mineral prices into the system.
    /// </summary>
    /// <example>
    ///  glob glob Silver is 34 Credits           
    /// </example>
    public class MineralPriceImporterTask : ITask
    {
        private const string MatchingPattern = @"(.+) is (\w+) Credits";
        //// (.+) is one or more any characters (including whitespace)
        //// (\w+) is one or more any characters (no whitespaces - just one word)

        private readonly Context context;

        private readonly AlienToArabicConvertor alienToArabicConvertor;

        public MineralPriceImporterTask(Context context, AlienToArabicConvertor alienToArabicConvertor)
        {
            this.context = context;
            this.alienToArabicConvertor = alienToArabicConvertor;
        }

        public bool CanRun(string inputLine)
        {
            if (inputLine.IsQuestion())
            {
                return false;
            }

            return inputLine.IsMatch(MatchingPattern);
        }

        public void Run(string inputLine)
        {
            var extractedData = inputLine.MatchTwoGroups(MatchingPattern);
            if (extractedData == null)
            {
                throw new ArgumentException("Input line cannot be parsed by MineralPriceImporterTask:" + inputLine);
            }

            var mineralNameAndAmountAsString = extractedData.Item1;
            string totalPriceAsString = extractedData.Item2;

            decimal totalPriceAsNumber;
            if (!decimal.TryParse(totalPriceAsString, out totalPriceAsNumber))
            {
                throw new ArgumentException("Price is not a number:" + totalPriceAsNumber);
            }

            var mineralNameAndAmount = LineParsingUtility.Split(mineralNameAndAmountAsString); // "glob glob Silver"

            IEnumerable<string> mineralAmountAsAlienNumber = mineralNameAndAmount.WithoutLast(); // "glob glob"
            string mineralName = mineralNameAndAmount.Last(); // "Silver"

            var mineralAmount = this.alienToArabicConvertor.Convert(mineralAmountAsAlienNumber);

            var pricePerUnit = totalPriceAsNumber / mineralAmount;

            context.MineralPricesPerUnit[mineralName] = pricePerUnit;
        }
    }
}
