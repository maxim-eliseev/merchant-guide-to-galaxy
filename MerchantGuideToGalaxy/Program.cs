namespace MerchantGuideToGalaxy
{
    using System;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            var inputfileName = "input.txt";
            if (!File.Exists(inputfileName))
            {
                Console.WriteLine("File does not exist:" + inputfileName);
                Console.ReadKey();
                return;
            }

            var input = File.ReadAllLines(inputfileName);
            var output = new Processor(input).Process();
            File.WriteAllLines("output.txt", output);
        }
    }
}
