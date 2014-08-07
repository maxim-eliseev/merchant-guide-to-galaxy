namespace MerchantGuideToGalaxy
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using MerchantGuideToGalaxy.Core;
    using MerchantGuideToGalaxy.DependencyInjection;

    using Ninject;

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
                ProcessInputFile();
            }

            Console.ReadKey();
        }

        private static void ProcessInputFile()
        {
            var input = File.ReadAllLines(InputFileName);

            var output = ProcessInputData(input);

            File.WriteAllLines(OutputFileName, output);

            output.ToList().ForEach(Console.WriteLine);
        }

        private static IEnumerable<string> ProcessInputData(IEnumerable<string> input)
        {
            IKernel kernel = new StandardKernel(new NinjectConfigurationModule());
            var processor = kernel.Get<Processor>();
            var output = processor.Process(input);
            return output;
        }
    }
}
