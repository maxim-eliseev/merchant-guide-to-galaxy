namespace MerchantGuideToGalaxy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MerchantGuideToGalaxy.Tasks;
    using MerchantGuideToGalaxy.Utils;

    public class Processor
    {
        private readonly IEnumerable<string> input;

        private readonly Context context;

        private readonly TaskFactory taskFactory;

        public Processor(IEnumerable<string> input) :
            this(input, new Context())
        {
        }

        public Processor(IEnumerable<string> input, Context context)
        {
            this.input = input;
            this.context = context;
            this.taskFactory = new TaskFactory(context);
        }

        public IEnumerable<string> Process()
        {
            if (!this.input.Any())
            {
                this.context.Output.Add("Input is empty");
                return this.context.Output;
            }

            if (!this.input.Any(LineParsingUtility.IsQuestion))
            {
                this.context.Output.Add("Input has no questions");
                return this.context.Output;
            }

            foreach (var inputLine in this.input)
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