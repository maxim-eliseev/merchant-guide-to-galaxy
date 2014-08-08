namespace MerchantGuideToGalaxy.Utils
{
    using System;

    public class ParsingException : Exception
    {
        public ParsingException(string message)
            : base(message)
        {
        }

        public ParsingException(string format, string[] args)
            : base(string.Format(format, args))
        {
        }
    }
}
