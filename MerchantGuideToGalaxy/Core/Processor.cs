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

        public Processor() :
            this(new Context())
        {
        }

        public Processor(Context context)
        {
            this.context = context;
            this.taskFactory = new TaskFactory(context);
        }

        public IEnumerable<string> Process(IEnumerable<string> input)
        {
            this.context.Clear();

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
            catch (ArgumentException e)
            {
                this.context.Output.Add(ErrorMessageTask.GenericMessage);
                this.context.Output.Add("Details: " + e.Message);
            }
        }
    }
}