namespace MerchantGuideToGalaxy.Tasks
{
    using MerchantGuideToGalaxy.Utils;

    public class AlienNumberImporterTask : ITask
    {
        private const string MatchingPattern = @"(\w+) is (\w+)";
        //// \w+ is one or more word characters. Only one word is matched (since whitespaces are not allowed)
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

            return inputLine.IsMatch(MatchingPattern);
        }

        ////  glob is I            
        public void Run(string inputLine)
        {
            var extractedData = inputLine.MatchTwoGroups(MatchingPattern);
            var alienSymbol = extractedData.Item1;
            var romanSymbol = extractedData.Item2;  // We might want to validate roman symbol

            this.context.AlienToRomanNumberMap[alienSymbol] = romanSymbol;
        }
    }
}
