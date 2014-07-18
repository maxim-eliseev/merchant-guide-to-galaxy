﻿namespace MerchantGuideToGalaxy.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class AlienToRomanConvertor
    {
        private readonly IDictionary<string, string> alienToRomanNumberMap;

        public AlienToRomanConvertor(Context context)
        {
            this.alienToRomanNumberMap = context.AlienToRomanNumberMap;
        }

        public void AddAlienSymbol(string alienSymbol, string romanSymbol)
        {
            this.alienToRomanNumberMap[alienSymbol] = romanSymbol;
        }

        public string Convert(IEnumerable<string> alienSymbols)
        {
            IEnumerable<string> romanSymbols = alienSymbols.Select(this.Convert);
            var romanNumber = string.Join(string.Empty, romanSymbols);
            return romanNumber;
        }

        private string Convert(string alienSymbol)
        {
            if (!this. alienToRomanNumberMap.ContainsKey(alienSymbol))
            {
                throw new ArgumentException("Alien symbol not found:" + alienSymbol);
            }

            return this.alienToRomanNumberMap[alienSymbol];
        }

    }
}
