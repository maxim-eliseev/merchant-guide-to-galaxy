namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConsoleWrapper : IConsoleWrapper
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }

        public void WriteLines(IEnumerable<string> lines)
        {
            lines.ToList().ForEach(this.WriteLine);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
