namespace MerchantGuideToGalaxy.Utils
{
    public static class LineParsingUtility
    {
        public const string NumberQuestionStart = "How much is";

        public static bool IsQuestion(string line)
        {
            return line.Contains("?");
        }

    }
}
