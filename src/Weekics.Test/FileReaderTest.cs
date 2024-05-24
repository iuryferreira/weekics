using Weekics.Cli.Core.Helpers;

namespace Weekics.Test;

public class FileReaderTest
{
    [Fact(DisplayName = "ReadFileAsync should log error when file not found")]
    public async Task ReadFileAsync_ShouldLogError_WhenFileNotFound()
    {
        //Arrange
        var logger = new Mock<ILogger<FileReader>>();
        var fileReader = new FileReader(logger.Object);

        // Act
        var result = await fileReader.ReadFileAsync("file-not-found.txt");

        // Assert
        LoggerFixture.VerifyLogger(logger, LogLevel.Error, "File not found");
        Assert.Empty(result);
    }

    [Fact(DisplayName = "ReadFileAsync should return file content when file found")]
    public async Task ReadFileAsync_ShouldReturnFileContent_WhenFileFound()
    {
        //Arrange
        var logger = new Mock<ILogger<FileReader>>();
        var fileReader = new FileReader(logger.Object);
        var path = FileReaderFixture.CreateTempFile("Hello World\nHello Brazil");

        // Act
        var result = await fileReader.ReadFileAsync(path);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Count);

        FileReaderFixture.CleanupTempFile(path);
    }

   
}