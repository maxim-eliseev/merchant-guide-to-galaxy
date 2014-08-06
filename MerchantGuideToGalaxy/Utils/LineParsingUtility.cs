namespace MerchantGuideToGalaxy.Utils
{
    public static class LineParsingUtility
    {
        public const string AlienNumberQuestionStart = "How much is";
        public const string GoodsPriceQuestionStart = "how many Credits is";

        public static bool IsQuestion(string line)
        {
            return line.Contains("?");
        }

    }
}
