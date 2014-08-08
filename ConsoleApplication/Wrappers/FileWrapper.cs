namespace ConsoleApplication.Wrappers
{
    using System.Collections.Generic;
    using System.IO;

    public class FileWrapper : IFileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public IEnumerable<string> ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
