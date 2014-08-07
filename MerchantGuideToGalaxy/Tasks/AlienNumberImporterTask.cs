namespace MerchantGuideToGalaxy.Tasks
{
    using MerchantGuideToGalaxy.Utils;

    /// <summary>
    /// Processes lines which import alien symbols into system.
    /// </summary>
    /// <example>
    ///  glob is I            
    /// </example>
    public class AlienNumberImporterTask : ITask
    {
        private const string MatchingPattern = @"(.+) is (.+)";
        //// .+ is one or more characters. Mutpliple words may be matched (since whitespaces are allowed)
        //// () indicates a capturing group

        private readonly Context context;

        public AlienNumberImporterTask(Context context)
        {
            this.context = context;
        }

        public bool CanRun(string inputLine)
        {
            if (inputLine.IsQuestion())
            {
                return false;
            }

            var extractedData = inputLine.MatchTwoGroups(MatchingPattern);
            if (extractedData == null)
            {
                return false;
            }

            // There are two groups: before "is" and after "is" (see MatchingPattern)
            // Both must consist of exactly one word
            if (extractedData.Item1.WordsCount() == 1 && extractedData.Item2.WordsCount() == 1)
            {
                return true;
            }

            return false;
        }
        
        public void Run(string inputLine)
        {
            var extractedData = inputLine.MatchTwoGroups(MatchingPattern);
            var alienSymbol = extractedData.Item1;
            var romanSymbol = extractedData.Item2;  // We might want to validate roman symbol

            this.context.AlienToRomanNumberMap[alienSymbol] = romanSymbol;
        }
    }
}
