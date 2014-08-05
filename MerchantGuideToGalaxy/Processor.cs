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

            foreach (var inputLine in input)
            {
                ProcessCommand(inputLine);
            }

            return this.context.Output;
        }

        private void ProcessCommand(string inputLine)
        {
            var commandType = GetCommandType(inputLine);
            switch (commandType)
            {
                case CommandType.NumberEntry:
                    DoNumberEntry(inputLine);
                    break;
                case CommandType.NumberQuestion:
                    this.AnswerNumberQuestion(inputLine);                    
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        ////  glob is I
        private void DoNumberEntry(string inputLine)
        {
            var t = new AlienNumberParsingTask(context);
            t.Run(inputLine);
        }

        //// how much is pish tegj glob glob ?
        private void AnswerNumberQuestion(string inputLine)
        {
            var t = new AlienNumberConversionResponderTask(context);
            t.Run(inputLine);
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