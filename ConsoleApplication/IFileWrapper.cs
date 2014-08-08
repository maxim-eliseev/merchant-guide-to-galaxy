namespace ConsoleApplication
{
    using System.Collections.Generic;

    public interface IFileWrapper
    {
        bool Exists(string path);

        IEnumerable<string> ReadAllLines(string path);
    }
}