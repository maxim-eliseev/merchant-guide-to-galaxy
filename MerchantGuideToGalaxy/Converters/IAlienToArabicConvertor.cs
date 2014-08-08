namespace MerchantGuideToGalaxy.Converters
{
    using System.Collections.Generic;

    public interface IAlienToArabicConvertor
    {
        int Convert(IEnumerable<string> alienSymbols);
    }
}