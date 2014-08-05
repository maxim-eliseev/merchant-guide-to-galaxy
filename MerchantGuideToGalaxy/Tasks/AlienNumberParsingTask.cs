using System;
using System.Collections.Generic;

namespace MerchantGuideToGalaxy.Tasks
{
    public class AlienNumberParsingTask : ITask
    {
        private readonly Context context;

        public AlienNumberParsingTask(Context context)
        {
            this.context = context;
        }

        public void Run(string inputLine)
        {
            var words = inputLine.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            var alienSymbol = words[0];
            if (words[1] != "is")
            {
                throw new ArgumentException("Wrong format:" + inputLine);
            }

            var romanSymbol = words[2];

            this.context.AlienToRomanNumberMap[alienSymbol] = romanSymbol;
        }
    }
}
