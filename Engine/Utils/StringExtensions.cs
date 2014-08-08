namespace MerchantGuideToGalaxy.Utils
{
    using System;

    public static class StringExtensions
    {
        // http://stackoverflow.com/a/17563994/1131855
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
