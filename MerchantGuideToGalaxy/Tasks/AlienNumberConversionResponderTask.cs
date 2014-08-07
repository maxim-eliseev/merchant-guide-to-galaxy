using System;
using System.Collections.Generic;

namespace MerchantGuideToGalaxy.Tasks
{
    using System.Diagnostics.CodeAnalysis;
    using System.Text.RegularExpressions;

    using MerchantGuideToGalaxy.Converters;
    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.Utils;

    /// <summary>
    /// Processes lines which ask questions to convert an alien number to an arabic number
    /// </summary>
    /// <example>
    /// how much is pish tegj glob glob ?        
    /// </example>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class AlienNumberConversionResponderTask : ITask
    {
        private const string MatchingPattern = @"how much is ([\w\s]+)?";
        //// [\w\s]+ is one or more word characters OR whitespaces. This is done to exclude "?" from matching.
        //// () indicates a capturing group

        private readonly Context context;

        private readonly AlienToArabicConvertor alienToArabicConvertor;
        
        public AlienNumberConversionResponderTask(Context context, AlienToArabicConvertor alienToArabicConvertor)
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
            var alienNumberAsString = inputLine.MatchOneGroup(MatchingPattern);

            var alienNumber = LineParsingUtility.Split(alienNumberAsString);
            var arabicNumber = alienToArabicConvertor.Convert(alienNumber);
            var response = alienNumberAsString.Trim() + " is " + arabicNumber; // Trim is to remove possible trailing spaces
            context.Output.Add(response);
        }
    }
}
