namespace MerchantGuideToGalaxy.Converters
{
    using System;
    using System.Collections.Generic;

    public class RomanToArabicConvertor
    {
        private readonly Dictionary<char, int> romanToArabicNumberMap = new Dictionary<char, int>();
        private readonly Dictionary<string, string> subtractionNotationRomanToSimpleRomanMap = new Dictionary<string, string>();

        public RomanToArabicConvertor()
        {
            this.romanToArabicNumberMap['I'] = 1;
            this.romanToArabicNumberMap['V'] = 5;
            this.romanToArabicNumberMap['X'] = 10;
            this.romanToArabicNumberMap['L'] = 50;
            this.romanToArabicNumberMap['C'] = 100;
            this.romanToArabicNumberMap['D'] = 500;
            this.romanToArabicNumberMap['M'] = 1000;

            this.subtractionNotationRomanToSimpleRomanMap["IV"] = "IIII";
            this.subtractionNotationRomanToSimpleRomanMap["IX"] = "VIIII";

            this.subtractionNotationRomanToSimpleRomanMap["XL"] = "XXXX";
            this.subtractionNotationRomanToSimpleRomanMap["XC"] = "LXXXX";

            this.subtractionNotationRomanToSimpleRomanMap["CD"] = "CCCC";
            this.subtractionNotationRomanToSimpleRomanMap["CM"] = "DCCCC";
        }

        public int Convert(string romanNumber)
        {
            foreach (var kvp in this.subtractionNotationRomanToSimpleRomanMap)
            {
                var subtractivePair = kvp.Key; // eg IV
                var addableSubstitute = kvp.Value; // eg IIII
                romanNumber = romanNumber.Replace(subtractivePair, addableSubstitute);
            }

            //// Now all symbols of the roman number are addable. Each symbol represents one arabic number.

            int total = 0;
            foreach (char romanSymbol in romanNumber)
            {
                int arabicValue = this.Convert(romanSymbol);
                total += arabicValue;
            }

            return total;
        }

        private int Convert(char romanSymbol)
        {
            if (!this.romanToArabicNumberMap.ContainsKey(romanSymbol))
            {
                throw new ArgumentException("Roman symbol not found:" + romanSymbol);
            }

            return this.romanToArabicNumberMap[romanSymbol];
        }
    }
}
