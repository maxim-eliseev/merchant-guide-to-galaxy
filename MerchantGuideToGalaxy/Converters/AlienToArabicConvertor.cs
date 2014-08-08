namespace MerchantGuideToGalaxy.Converters
{
    using System.Collections.Generic;

    public class AlienToArabicConvertor : IAlienToArabicConvertor
    {
        private readonly IAlienToRomanConvertor alienToRomanConvertor;

        private readonly IRomanToArabicConvertor romanToArabicConvertor;

        public AlienToArabicConvertor(IAlienToRomanConvertor alienToRomanConvertor, IRomanToArabicConvertor romanToArabicConvertor)
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
