namespace MerchantGuideToGalaxy.Core
{
    using System.Collections.Generic;

    public interface IProcessor
    {        
        IEnumerable<string> Process(IEnumerable<string> input);
    }
}