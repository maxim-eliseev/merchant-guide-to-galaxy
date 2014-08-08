namespace MerchantGuideToGalaxy.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MerchantGuideToGalaxy.Core;

    public class AlienToRomanConvertor
    {
        private readonly IContext context;

        public AlienToRomanConvertor(IContext context)
        {
            this.context = context;
        }

        public string Convert(IEnumerable<string> alienSymbols)
        {
            IEnumerable<string> romanSymbols = alienSymbols.Select(this.Convert);
            var romanNumber = string.Join(string.Empty, romanSymbols);
            return romanNumber;
        }

        private string Convert(string alienSymbol)
        {
            if (!this.context.AlienToRomanNumberMap.ContainsKey(alienSymbol))
            {
                throw new ArgumentException("Alien symbol not found:" + alienSymbol);
            }

            return this.context.AlienToRomanNumberMap[alienSymbol];
        }
    }
}
