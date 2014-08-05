using System;
using System.Collections.Generic;

namespace MerchantGuideToGalaxy.Tasks
{
    using System.Text.RegularExpressions;

    using MerchantGuideToGalaxy.Converters;

    public class AlienNumberConversionResponderTask : ITask
    {
        private const string NumberQuestionStart = "How much is";

        private readonly Context context;

        private readonly RomanToArabicConvertor romanToArabicConvertor;

        private readonly AlienToRomanConvertor alienToRomanConvertor;


        public AlienNumberConversionResponderTask(Context context)
        {
            this.context = context;
            romanToArabicConvertor = new RomanToArabicConvertor();
            alienToRomanConvertor = new AlienToRomanConvertor(context);
        }

        public void Run(string inputLine)
        {
            var preparedInputline = Regex.Replace(inputLine, NumberQuestionStart, string.Empty, RegexOptions.IgnoreCase);
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
