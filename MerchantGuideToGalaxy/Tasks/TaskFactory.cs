namespace MerchantGuideToGalaxy.Tasks
{
    using System;

    using MerchantGuideToGalaxy.Utils;

    public class TaskFactory
    {
        private readonly Context context;

        public TaskFactory(Context context)
        {
            this.context = context;
        }

        public ITask CreateTask(string inputLine)
        {
            //// Please note that order of clauses below is important.

            //// how much is pish tegj glob glob ?
            if (inputLine.StartsWith(LineParsingUtility.NumberQuestionStart, StringComparison.CurrentCultureIgnoreCase) && LineParsingUtility.IsQuestion(inputLine))
            {
                return new AlienNumberConversionResponderTask(this.context);
            }

            ////  glob is I            
            if (inputLine.Contains("is", StringComparison.CurrentCultureIgnoreCase))
            {
                return new AlienNumberImporterTask(this.context);
            }
             
            throw new NotImplementedException();
        }
    }
}