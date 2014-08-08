namespace MerchantGuideToGalaxy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MerchantGuideToGalaxy.Tasks;
    using MerchantGuideToGalaxy.Utils;

    public class Processor
    {
        private readonly Context context;

        private readonly TaskFactory taskFactory;

        public Processor(Context context, TaskFactory taskFactory)
        {
            this.context = context;
            this.taskFactory = taskFactory;
        }

        public IEnumerable<string> Process(IEnumerable<string> input)
        {
            this.context.Clear(); // This is to ensure subsequent calls do not reuse context from previous calls

            if (!input.Any())
            {
                this.context.Output.Add("Input is empty");
                return this.context.Output;
            }

            if (!input.Any(LineParsingUtility.IsQuestion))
            {
                this.context.Output.Add("Input has no questions");
                return this.context.Output;
            }

            foreach (var inputLine in input)
            {
                this.ProcessLine(inputLine);
            }

            return this.context.Output;
        }

        private void ProcessLine(string inputLine)
        {
            var task = this.taskFactory.CreateTask(inputLine);

            try
            {
                task.Run(inputLine);
            }
            catch (ParsingException e)
            {
                this.context.Output.Add(ErrorMessageTask.GenericMessage);
                this.context.Output.Add("Details: " + e.Message);
            }
        }
    }
}