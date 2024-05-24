namespace Weekics.Cli.Core.Helpers;

public interface IFileReader
{
    Task<IReadOnlyList<string>> ReadFileAsync(string path);
}

class FileReader(ILogger<FileReader> logger) : IFileReader
{

    public async Task<IReadOnlyList<string>> ReadFileAsync(string path)
    {
        if (!File.Exists(path))
        {
            logger.LogError("File not found: {path}", path);
            return [];
        }

        var lines = await File.ReadAllLinesAsync(path);

        logger.LogInformation("File read: {path}", path);
        logger.LogDebug("File content: {text}", string.Join(Environment.NewLine, lines));
        return lines;
    }
}

public static class DateExtensions
{
    public static DateOnly GetNextWeekday(this DateOnly start, DayOfWeek day)
    {
        int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
        if (daysToAdd == 0)  daysToAdd = 7;
        return start.AddDays(daysToAdd);
    }

    public static DateOnly GetNextWeekday(this DateOnly start, string day)
    {
        return GetNextWeekday(start, Enum.Parse<DayOfWeek>(day));
    }


}
