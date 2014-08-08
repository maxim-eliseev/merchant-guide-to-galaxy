namespace MerchantGuideToGalaxy.Core
{
    using System.Collections.Generic;

    public class Context
    {
        public readonly IDictionary<string, string> AlienToRomanNumberMap = new Dictionary<string, string>();

        public readonly IDictionary<string, decimal> MineralPricesPerUnit = new Dictionary<string, decimal>();

        public readonly IList<string> Output = new List<string>();

        public void Clear()
        {
            this.AlienToRomanNumberMap.Clear();
            this.MineralPricesPerUnit.Clear();
            this.Output.Clear();
        }
    }
}
