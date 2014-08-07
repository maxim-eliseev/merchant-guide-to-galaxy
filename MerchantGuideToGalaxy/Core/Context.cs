namespace MerchantGuideToGalaxy.Core
{
    using System.Collections.Generic;

    public class Context
    {
        public readonly IDictionary<string, string> AlienToRomanNumberMap = new Dictionary<string, string>();

        public readonly IDictionary<string, decimal> GoodsPricesPerUnit = new Dictionary<string, decimal>();

        public readonly IList<string> Output = new List<string>();
    }
}
