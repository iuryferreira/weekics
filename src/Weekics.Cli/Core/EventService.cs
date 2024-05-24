using Weekics.Cli.Core.Entities;
using Weekics.Cli.Core.Helpers;

namespace Weekics.Cli.Core;

public interface IEventService
{
    Task<IEnumerable<Event>> GetEventsAsync(string path);
}


class EventService(ILogger<EventService> logger, IFileReader fileReader) : IEventService
{
    private readonly ILogger<EventService> logger = logger;
    private readonly IFileReader fileReader = fileReader;

    public async Task<IEnumerable<Event>> GetEventsAsync(string path)
    {
        var raw = await fileReader.ReadFileAsync(path);

        if (raw.Count == 0)
        {
            return [];
        }

        var events = CastContentToEvents(raw);
        logger.LogInformation("Events loaded: {count}", events.Count());

        return events;
    }

    private static List<Event> CastContentToEvents(IReadOnlyList<string> lines)
    {
        var events = new List<Event>();

        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        foreach (var line in lines)
        {
            if (line.StartsWith("####"))
            {
                string dayOfWeek = line[5..];
                currentDate = currentDate.GetNextWeekday(dayOfWeek);
                continue;
            }

            if (line.StartsWith("- `"))
            {
                var timeAndEvent = line[3..];
                timeAndEvent = timeAndEvent.Replace("`","").Trim();
                var parts = timeAndEvent.Split('|');
                var timeRange = parts[0].Trim();
                var eventName = parts[1].Trim();

                var times = timeRange.Split('-');
                var startTime = TimeOnly.ParseExact(times[0].Trim(), "HH:mm", CultureInfo.InvariantCulture);
                var endTime = TimeOnly.ParseExact(times[1].Trim(), "HH:mm", CultureInfo.InvariantCulture);

                events.Add(new Event
                {
                    Name = eventName,
                    Date = currentDate,
                    StartAt = startTime,
                    EndAt = endTime
                });

            }
        }

        return events;
    }
}
