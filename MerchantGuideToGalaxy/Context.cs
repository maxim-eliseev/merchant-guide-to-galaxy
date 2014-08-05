using System.Collections.Generic;

namespace MerchantGuideToGalaxy
{
    public class Context
    {
        public readonly IDictionary<string, string> AlienToRomanNumberMap = new Dictionary<string, string>();

        public readonly IList<string> Output = new List<string>();
    }
}
