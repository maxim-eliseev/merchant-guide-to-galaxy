namespace MerchantGuideToGalaxy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MerchantGuideToGalaxy.Tasks;
    using MerchantGuideToGalaxy.Utils;

    public class Processor : IProcessor
    {
        private readonly IContext context;

        private readonly TaskFactory taskFactory;

        public Processor(IContext context, TaskFactory taskFactory)
        {
            this.context = context;
            this.taskFactory = taskFactory;
        }

        public IEnumerable<string> Process(IEnumerable<string> input)
        {
            context.Output.Clear();

            if (!input.Any())
            {
                this.context.Output.Add("Input is empty");
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