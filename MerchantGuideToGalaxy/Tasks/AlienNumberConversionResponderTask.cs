using System;
using System.Collections.Generic;

namespace MerchantGuideToGalaxy.Tasks
{
    using System.Text.RegularExpressions;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Utils;

    public class AlienNumberConversionResponderTask : ITask
    {
        private readonly Context context;

        private readonly RomanToArabicConvertor romanToArabicConvertor;

        private readonly AlienToRomanConvertor alienToRomanConvertor;


        public AlienNumberConversionResponderTask(Context context)
        {
            this.context = context;
            romanToArabicConvertor = new RomanToArabicConvertor();
            alienToRomanConvertor = new AlienToRomanConvertor(context);
        }

        //// how much is pish tegj glob glob ?
        public void Run(string inputLine)
        {
            var preparedInputline = Regex.Replace(inputLine, LineParsingUtility.AlienNumberQuestionStart, string.Empty, RegexOptions.IgnoreCase);
            preparedInputline = preparedInputline.Replace("?", string.Empty);
            preparedInputline = preparedInputline.Trim();
            var alienNumber = preparedInputline;

            string[] alienSymbols = alienNumber.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var arabicNumber = ConvertAlienToArabic(alienSymbols);
            var response = alienNumber + " is " + arabicNumber;
            context.Output.Add(response);
        }

        private int ConvertAlienToArabic(IEnumerable<string> alienSymbols)
        {
            var romanNumber = alienToRomanConvertor.Convert(alienSymbols);
            int arabic = romanToArabicConvertor.Convert(romanNumber);
            return arabic;
        }
    }
}
