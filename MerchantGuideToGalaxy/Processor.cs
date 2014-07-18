namespace MerchantGuideToGalaxy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Processor
    {
        private const string NumberQuestionStart = "How much is";

        private readonly IEnumerable<string> input;

        private readonly RomanToArabicConvertor romanToArabicConvertor;

        private readonly List<string> output = new List<string>();
        private readonly Dictionary<string,string> alienToRomanNumberMap = new Dictionary<string, string>();

        public Processor(IEnumerable<string> input):
            this(input, new RomanToArabicConvertor())
        {
        }

        public Processor(IEnumerable<string> input, RomanToArabicConvertor romanToArabicConvertor)
        {
            this.input = input;
            this.romanToArabicConvertor = romanToArabicConvertor;
        }

        public IEnumerable<string> Process()
        {
            if (!input.Any())
            {
                this.output.Add("Input is empty");
                return output;
            }

            if (!input.Any(IsQuestion))
            {
                this.output.Add("Input has no questions");
                return output;
            }

            foreach (var inputLine in input)
            {
                ProcessCommand(inputLine);
            }

            return this.output;
        }

        private void ProcessCommand(string inputLine)
        {
            var commandType = GetCommandType(inputLine);
            switch (commandType)
            {
                case CommandType.NumberEntry:
                    DoNumberEntry(inputLine);
                    break;
                case CommandType.NumberQuestion:
                    var response = AskNumberQuestion(inputLine);
                    output.Add(response);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        ////  glob is I
        private void DoNumberEntry(string inputLine)
        {
            var words = inputLine.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
            var alienNumber = words[0];
            if (words[1] != "is")
            {
                throw new ArgumentException("Wrong format:" + inputLine);
            }
            var romanNumber = words[2];

            alienToRomanNumberMap[alienNumber] = romanNumber;
        }

        //// how much is pish tegj glob glob ?
        private string AskNumberQuestion(string inputLine)
        {
            var preparedInputline = Regex.Replace(inputLine, NumberQuestionStart, string.Empty, RegexOptions.IgnoreCase);
            preparedInputline = preparedInputline.Replace("?", string.Empty);
            preparedInputline = preparedInputline.Trim();
            var alienNumber = preparedInputline;

            string[] alienDigits = alienNumber.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var arabicNumber = ConvertAlienToArabic(alienDigits);
            return alienNumber + " is " + arabicNumber;
        }

        private int ConvertAlienToArabic(IEnumerable<string> alienDigits)
        {
            var romanNumber = ConvertAlienToRoman(alienDigits);
            int arabic = romanToArabicConvertor.Convert(romanNumber);
            return arabic;
        }

        private string ConvertAlienToRoman(IEnumerable<string> alienDigits)
        {
            IEnumerable<string> romanDigits = alienDigits.Select(ConvertAlienToRoman);
            var romanNumber = string.Join(string.Empty, romanDigits);
            return romanNumber;
        }

        private string ConvertAlienToRoman(string alienDigit)
        {
            if (!this.alienToRomanNumberMap.ContainsKey(alienDigit))
            {
                throw new ArgumentException("Alien digit not found:" + alienDigit);
            }

            return alienToRomanNumberMap[alienDigit];
        }
    

        private CommandType? GetCommandType(string inputLine)
        {
            if (inputLine.StartsWith(NumberQuestionStart, StringComparison.CurrentCultureIgnoreCase) && this.IsQuestion(inputLine))
            {
                return CommandType.NumberQuestion;
            }
            if (inputLine.Contains("is", StringComparison.CurrentCultureIgnoreCase))
            {
                return CommandType.NumberEntry;
            }

            return null;
        }

        private bool IsQuestion(string line)
        {
            return line.Contains("?");
        }

        public enum CommandType
        {
            NumberEntry,
            NumberQuestion
        }
    }
}