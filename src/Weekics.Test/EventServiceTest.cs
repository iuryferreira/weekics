using Weekics.Cli.Core;
using Weekics.Cli.Core.Helpers;

namespace Weekics.Test;

public class EventServiceTest
{
    [Fact(DisplayName = "GetEventsAsync should return empty event list when no events found")]
    public async Task GetEventsAsync_ShouldReturnEmptyEventList_WhenNoEventsFound()
    {
        //Arrange
        var logger = new Mock<ILogger<EventService>>();
        var fileReader = new Mock<IFileReader>();
        var eventService = new EventService(logger.Object, fileReader.Object);

        fileReader.Setup(x => x.ReadFileAsync(It.IsAny<string>())).ReturnsAsync([]);

        // Act
        var result = await eventService.GetEventsAsync("");

        // Assert
        Assert.Empty(result);
    }

    [Fact(DisplayName = "GetEventsAsync should return events when file events is valid")]
    public async Task GetEventsAsync_ShouldReturnEvents_WhenFileEventsIsValid()
    {
        //Arrange
        var logger = new Mock<ILogger<EventService>>();
        var fileReader = new Mock<IFileReader>();
        var eventService = new EventService(logger.Object, fileReader.Object);

        var path = FileReaderFixture.CreateTempFile("#### Monday\n- `09:00 - 10:00` | Event 1\n- `10:00 - 11:00` | Event 2\n#### Tuesday\n- `09:00 - 10:00` | Event 3\n- `10:00 - 11:00` | Event 4");

        fileReader.Setup(x => x.ReadFileAsync(It.IsAny<string>())).ReturnsAsync(File.ReadAllLines(path));

        // Act
        var result = await eventService.GetEventsAsync(path);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(4, result.Count());

        FileReaderFixture.CleanupTempFile(path);
    }

}