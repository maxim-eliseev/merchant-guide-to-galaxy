namespace MerchantGuideToGalaxy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MerchantGuideToGalaxy.Tasks;
    using MerchantGuideToGalaxy.Utils;

    public class Processor
    {
        private const string NumberQuestionStart = "How much is";

        private readonly IEnumerable<string> input;

        private readonly Context context;

        public Processor(IEnumerable<string> input) :
            this(input, new Context())
        {
        }

        public Processor(IEnumerable<string> input, Context context)
        {
            this.input = input;
            this.context = context;
        }

        public IEnumerable<string> Process()
        {
            if (!this.input.Any())
            {
                this.context.Output.Add("Input is empty");
                return this.context.Output;
            }

            if (!this.input.Any(this.IsQuestion))
            {
                this.context.Output.Add("Input has no questions");
                return this.context.Output;
            }

            foreach (var inputLine in this.input)
            {
                var task = this.CreateTask(inputLine);
                task.Run(inputLine);
            }

            return this.context.Output;
        }

        private ITask CreateTask(string inputLine)
        {
            ITask task;

            var commandType = this.GetCommandType(inputLine);
            switch (commandType)
            {
                case CommandType.NumberEntry:
                    ////  glob is I
                    task = new AlienNumberParsingTask(this.context);
                    break;
                case CommandType.NumberQuestion:
                    //// how much is pish tegj glob glob ?
                    task = new AlienNumberConversionResponderTask(this.context);
                    break;
                default:
                    throw new System.NotImplementedException();
            }
            return task;
        }

        private CommandType? GetCommandType(string inputLine)
        {
            if (inputLine.StartsWith(NumberQuestionStart, StringComparison.CurrentCultureIgnoreCase) && this.IsQuestion(inputLine))
            {
                return CommandType.NumberQuestion;
            }
            if (inputLine.Contains("is", StringComparison.CurrentCultureIgnoreCase))
            {
                return CommandType.NumberEntry;
            }

            return null;
        }

        private bool IsQuestion(string line)
        {
            return line.Contains("?");
        }

        public enum CommandType
        {
            NumberEntry,
            NumberQuestion
        }
    }
}