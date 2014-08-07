namespace MerchantGuideToGalaxy.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ComTypes;
    using System.Text.RegularExpressions;

    public static class LineParsingUtility
    {
        public const string AlienNumberQuestionStart = "How much is";
        public const string GoodsPriceQuestionStart = "how many Credits is";

        public static bool IsQuestion(this string line)
        {
            return line.Contains("?");
        }

        public static string MatchOneGroup(this string text, string pattern)
        {
            var groups = MatchGroups(text, pattern);
            if (groups == null)
            {
                return null;
            }

            if (groups.Count() != 1)
            {
                var message = string.Format("Parsing failure. The number of groups is not 1. Text:'{0}'. Pattern:'{1}'W", text, pattern);
                throw new ArgumentException(message);
            }

            return groups[0];
        }


        public static Tuple<string, string> MatchTwoGroups(this string text, string pattern)
        {
            var groups = MatchGroups(text, pattern);
            if (groups == null)
            {
                return null;
            }

            if (groups.Count() != 2)
            {
                var message = string.Format("Parsing failure. The number of groups is not 2. Text:'{0}'. Pattern:'{1}'W", text, pattern);
                throw new ArgumentException(message);
            }

            return new Tuple<string, string>(groups[0], groups[1]);
        }

        /// <summary>
        ///  Uses Regex to match a string with a pattern.
        /// </summary>
        /// <example>
        ///  string text = "glob is I";
        ///  string pattern = @"(\w+) is (\w+)";
        /// </example>        
        /// <remarks>        
        ///  (\w+) Matches one or more word characters. Only one word is included. () indicate a capturing group. It is included in m.Groups collection.
        ///  (.+) matches indicates one or more matches of any characters except whitespace. This is a capturing group also.
        ///  (?:is) Matches "is". This is non-capturing group. It is NOT included in m.Groups collection.
        ///  \s+ Matches one or more white-space characters.        
        /// </remarks>
        public static IList<string> MatchGroups(this string text, string pattern)
        {
            // Instantiate the regular expression object.
            var r = new Regex(pattern, RegexOptions.IgnoreCase);

            // Match the regular expression pattern against a text string.
            var m = r.Match(text);

            if (!m.Success)
            {
                return null;
            }

            if (r.Matches(text).Count > 1)
            {
                var message = string.Format("Parsing failure. More that one matches found in text:'{0}'. Pattern:'{1}'", text, pattern);
                throw new ArgumentException(message);
            }

            // Note that iteration starts from 1 since group 0 holds the whole text. It should be ignored.
            var list = new List<string>();
            for (int i = 1; i < m.Groups.Count; i++) 
            {
                list.Add( m.Groups[i].Value);
            }
            return list;
        }


        public static bool IsMatch(this string text, string pattern)
        {
            return Regex.IsMatch(text, pattern);
        }

        public static IList<string> Split(string s)
        {
            return s.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int WordsCount(this string s)
        {
            return Split(s).Count;
        }
    }
}
