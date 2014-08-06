namespace MerchantGuideToGalaxy
{
    using System;
    using System.IO;
    using System.Linq;

    public class Program
    {
        private const string InputFileName = "input.txt";
        private const string OutputFileName = "output.txt";

        public static void Main(string[] args)
        {
            if (!File.Exists(InputFileName))
            {
                Console.WriteLine("File does not exist:" + InputFileName);
            }
            else
            {
                var input = File.ReadAllLines(InputFileName);
                var output = new Processor(input).Process();

                File.WriteAllLines(OutputFileName, output);

                output.ToList().ForEach(Console.WriteLine);
            }

            Console.ReadKey();
        }
    }
}
