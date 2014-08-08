namespace ConsoleApplication.Wrappers
{
    using System.Collections.Generic;

    public interface IConsoleWrapper
    {
        void WriteLine(string line);
        void WriteLines(IEnumerable<string> line);

        string ReadLine();
    }
}