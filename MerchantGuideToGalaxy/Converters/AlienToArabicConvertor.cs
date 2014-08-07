namespace MerchantGuideToGalaxy.Converters
{
    using System.Collections.Generic;

    using MerchantGuideToGalaxy.Core;

    public class AlienToArabicConvertor
    {
        private readonly AlienToRomanConvertor alienToRomanConvertor;

        private readonly RomanToArabicConvertor romanToArabicConvertor;

        public AlienToArabicConvertor(AlienToRomanConvertor alienToRomanConvertor, RomanToArabicConvertor romanToArabicConvertor)
        {
            this.alienToRomanConvertor = alienToRomanConvertor;
            this.romanToArabicConvertor = romanToArabicConvertor;
        }

        public int Convert(IEnumerable<string> alienSymbols)
        {
            var romanNumber = alienToRomanConvertor.Convert(alienSymbols);
            int arabic = romanToArabicConvertor.Convert(romanNumber);
            return arabic;
        }
    }
}
