using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuideToGalaxy
{
    using MerchantGuideToGalaxy.Converters;

    public class Context
    {
        public readonly IDictionary<string, string> AlienToRomanNumberMap = new Dictionary<string, string>();
    }
}
