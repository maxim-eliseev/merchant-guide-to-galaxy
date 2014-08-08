namespace MerchantGuideToGalaxy.Core
{
    using System.Collections.Generic;

    public interface IContext
    {
        IDictionary<string, string> AlienToRomanNumberMap { get; }

        IDictionary<string, decimal> MineralPricesPerUnit { get; }

        IList<string> Output { get; }
    }
}