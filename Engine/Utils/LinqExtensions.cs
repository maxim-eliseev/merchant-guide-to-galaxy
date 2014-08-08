using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantGuideToGalaxy.Utils
{
    public static class LinqExtensions
    {
        // http://stackoverflow.com/a/4166561/1131855
        public static IEnumerable<T> WithoutLast<T>(this IEnumerable<T> xs)
        {
            T lastX = default(T);
            bool first = true;
            foreach (T x in xs)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    yield return lastX;
                }
                lastX = x;
            }
        }
    }
}
