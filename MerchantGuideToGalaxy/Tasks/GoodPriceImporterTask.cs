using System;

namespace MerchantGuideToGalaxy.Tasks
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using MerchantGuideToGalaxy.Converters;

    public class GoodPriceImporterTask : ITask
    {
        private readonly Context context;

        public GoodPriceImporterTask(Context context)
        {
            this.context = context;
        }

        ////  glob glob Silver is 34 Credits
        public void Run(string inputLine)
        {
           var words = inputLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var lastWordIndex = words.Count() - 1;

            if (words[lastWordIndex] != "Credits")
            {
                throw new ArgumentException("Wrong format:" + inputLine);
            }

            if (words[lastWordIndex - 2] != "is")
            {
                throw new ArgumentException("Wrong format:" + inputLine);
            }

            string totalPriceAsString = words[lastWordIndex - 1];
            decimal totalPriceAsNumber;
            if (!decimal.TryParse(totalPriceAsString, out totalPriceAsNumber))
            {
                throw new ArgumentException("Price is not a number:" + totalPriceAsNumber);
            }

            string goodName = words[lastWordIndex - 3]; // Just before "is"

            IEnumerable<string> alienNumber = words.Take(words.Count() - 4); // Everything before good name is an alien number (glob glob tegj ...)
            ValidateAlienNumber(alienNumber);

            var romanNumber = new AlienToRomanConvertor(context).Convert(alienNumber);
            var arabicNumber = new RomanToArabicConvertor().Convert(romanNumber);
            int numberOfUnits = arabicNumber;

            var pricePerUnit = totalPriceAsNumber / numberOfUnits;

            context.GoodsPricesPerUnit[goodName] = pricePerUnit;
        }

        private void ValidateAlienNumber(IEnumerable<string> alienNumber)
        {
            foreach (var alienSymbol in alienNumber)
            {
                if (!context.AlienToRomanNumberMap.ContainsKey(alienSymbol))
                {
                    throw new ArgumentException("Unknown alien symbol:" + alienSymbol);
                }
            }
        }
    }
}
