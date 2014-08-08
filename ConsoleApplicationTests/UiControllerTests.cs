namespace ConsoleApplicationTests
{
    using System.Collections.Generic;

    using ConsoleApplication;
    using ConsoleApplication.Wrappers;

    using MerchantGuideToGalaxy.Core;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UiControllerTests
    {
        private UiController uiController;

        private Mock<IProcessor> processor;
        private Mock<IConsoleWrapper> consoleWrapper;
        private Mock<IFileWrapper> fileWrapper;

        [TestInitialize]
        public void Init()
        {
            this.processor = new Mock<IProcessor>();
            this.consoleWrapper = new Mock<IConsoleWrapper>();
            this.fileWrapper = new Mock<IFileWrapper>();

            this.uiController = new UiController(this.processor.Object, this.consoleWrapper.Object, this.fileWrapper.Object);
        }

        [TestMethod]
        public void Given_input_file_when_Run_is_called_should_write_processor_output_to_console()
        {
            // Arrange
            var inputFileContent = new[] { "1", "2" };
            var inputFilePath = "someFile";
            this.fileWrapper.Setup(fw => fw.Exists(inputFilePath)).Returns(true);
            this.fileWrapper.Setup(fw => fw.ReadAllLines(inputFilePath)).Returns(inputFileContent);

            this.consoleWrapper.Setup(cw => cw.ReadLine()).Returns("exit");

            var processorResult = new[] { "A", "B" };
            this.processor.Setup(p => p.Process(inputFileContent)).Returns(processorResult);

            // Act
            this.uiController.Run(inputFilePath);

            // Assert
            this.consoleWrapper.Verify(cw => cw.WriteLines(processorResult));
        }

        [TestMethod]
        public void Given_console_input_when_Run_is_called_should_write_processor_output_to_console()
        {
            // Arrange
            this.fileWrapper.Setup(fw => fw.Exists(It.IsAny<string>())).Returns(false);

            var consoleInput = new Queue<string>();
            string line1 = "line1";
            consoleInput.Enqueue(line1);
            consoleInput.Enqueue("exit");

            this.consoleWrapper.Setup(cw => cw.ReadLine()).Returns(consoleInput.Dequeue); // http://stackoverflow.com/a/15235812/1131855

            var processorInput = new[] { line1 };
            string processorOutputForLine1 = "AAA1";
            var processorOutput = new[] { processorOutputForLine1 };
            this.processor.Setup(p => p.Process(processorInput)).Returns(processorOutput);

            // Act
            this.uiController.Run("AnyPath");

            // Assert
            this.consoleWrapper.Verify(cw => cw.WriteLines(processorOutput), Times.Once);
        }        
    }
}
