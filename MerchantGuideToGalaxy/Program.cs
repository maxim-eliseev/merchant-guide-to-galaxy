namespace MerchantGuideToGalaxy
{
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var output = new Processor(input).Process();
            File.WriteAllLines("output.txt", output);
        }
    }
}
