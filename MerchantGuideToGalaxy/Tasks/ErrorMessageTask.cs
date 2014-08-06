namespace MerchantGuideToGalaxy.Tasks
{
    public class ErrorMessageTask : ITask
    {
        public const string GenericMessage = "I have no idea what you are talking about";

        private readonly Context context;

        public ErrorMessageTask(Context context)
        {
            this.context = context;
        }

        public void Run(string line)
        {            
            context.Output.Add(GenericMessage);
        }
    }
}
