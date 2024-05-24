namespace Weekics.Test.Fixtures;
using Microsoft.Extensions.Logging;
using Moq;

public static class LoggerFixture
{
    public static void VerifyLogger<T>(Mock<ILogger<T>> logger, LogLevel logLevel, string message)
    {
        logger.Verify(x => x.Log(
            logLevel,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v!.ToString()!.Contains(message)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }
}