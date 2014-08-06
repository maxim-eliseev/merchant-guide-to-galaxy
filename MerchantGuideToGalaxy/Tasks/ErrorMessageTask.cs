namespace MerchantGuideToGalaxy.Tasks
{
    public class ErrorMessageTask : ITask
    {
        private Context context;

        public ErrorMessageTask(Context context)
        {
            this.context = context;
        }

        public void Run(string line)
        {
            var message = "I have no idea what you are talking about";
            context.Output.Add(message);
        }
    }
}
