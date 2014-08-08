namespace MerchantGuideToGalaxy.Converters
{
    using System.Collections.Generic;

    public interface IAlienToRomanConvertor
    {
        string Convert(IEnumerable<string> alienSymbols);
    }
}