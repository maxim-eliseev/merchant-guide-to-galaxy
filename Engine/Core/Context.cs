namespace MerchantGuideToGalaxy.Core
{
    using System.Collections.Generic;

    public class Context : IContext
    {
        public Context()
        {
            this.AlienToRomanNumberMap = new Dictionary<string, string>();
            this.MineralPricesPerUnit = new Dictionary<string, decimal>();
            this.Output = new List<string>();
        }

        public IDictionary<string, string> AlienToRomanNumberMap { get; private set; }

        public IDictionary<string, decimal> MineralPricesPerUnit { get; private set; }

        public IList<string> Output { get; private set; }
    }
}
