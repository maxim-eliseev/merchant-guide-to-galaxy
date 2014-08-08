namespace ConsoleApplication
{
    using System;
    using System.Collections.Generic;

    using ConsoleApplication.Wrappers;

    using MerchantGuideToGalaxy.Core;

    /// <summary>
    /// Reads data from an input file, outputs results and allows to run interactive queries
    /// </summary>
    public class UiController : IUiController
    {
        private readonly IProcessor processor;

        private readonly IConsoleWrapper console;

        private readonly IFileWrapper file;

        public UiController(IProcessor processor, IConsoleWrapper console, IFileWrapper file)
        {
            this.processor = processor;
            this.console = console;
            this.file = file;
        }

        public void Run(string inputFileName)
        {
            ProcessInputFile(inputFileName);

            this.WriteMessageToConsole("Enter your query below or type `exit` to exit");            

            this.ProcessConsoleInput();            
        }

        private void ProcessInputFile(string inputFileName)
        {
            if (!this.file.Exists(inputFileName))
            {
                this.WriteMessageToConsole("Input file does not exist:" + inputFileName);
            }
            else
            {
                var input = this.file.ReadAllLines(inputFileName);
                this.console.WriteLines(input);

                this.WriteMessageToConsole("Input file has been loaded:" + inputFileName);
                this.WriteMessageToConsole(string.Empty);

                this.ProcessLines(input);
                this.WriteMessageToConsole(string.Empty);
            }            
        }

        private void ProcessConsoleInput()
        {
            while (true)
            {
                var line = this.console.ReadLine();
                if (line.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
                {
                    break;
                }

                this.ProcessLines(new[] { line });
            }
        }

        private void ProcessLines(IEnumerable<string> input)
        {
            var output = this.processor.Process(input);
            this.console.WriteLines(output);            
        }

        private void WriteMessageToConsole(string message)
        {
            this.console.WriteLine("> " + message);
        }
    }
}
