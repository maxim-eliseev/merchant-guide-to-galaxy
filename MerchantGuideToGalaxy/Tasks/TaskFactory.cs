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
            if (
                inputLine.StartsWith(LineParsingUtility.AlienNumberQuestionStart, StringComparison.CurrentCultureIgnoreCase) && 
                LineParsingUtility.IsQuestion(inputLine))
            {
                return new AlienNumberConversionResponderTask(this.context);
            }

            //// how many Credits is glob prok Silver ?
            if (
                inputLine.StartsWith(LineParsingUtility.GoodsPriceQuestionStart, StringComparison.CurrentCultureIgnoreCase) &&
                LineParsingUtility.IsQuestion(inputLine))
            {
                return new GoodPriceResponderTask(this.context);
            }

            //// glob glob Silver is 34 Credits
            if (
                inputLine.Contains("Credits", StringComparison.CurrentCultureIgnoreCase) &&
                inputLine.Contains("is", StringComparison.CurrentCultureIgnoreCase)
                )
            {
                return new GoodPriceImporterTask(this.context);
            }

            ////  glob is I            
            if (inputLine.Contains("is", StringComparison.CurrentCultureIgnoreCase))
            {
                return new AlienNumberImporterTask(this.context);
            }

            return new ErrorMessageTask(context);
        }
    }
}