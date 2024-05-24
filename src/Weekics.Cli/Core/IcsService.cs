using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Weekics.Cli.Core.Entities;
using Calendar = Ical.Net.Calendar;

namespace Weekics.Cli.Core;

public interface IIcsService
{
    Task CreateWeekFile(IEnumerable<Event> events);
}

class IcsService(ILogger<IcsService> logger) : IIcsService
{
    public async Task CreateWeekFile(IEnumerable<Event> events)
    {
        if (events is null || !events.Any())
        {
            logger.LogWarning("No events to create week file");
            return;
        }

        logger.LogInformation("Creating week file");

        var calendar = new Calendar();

        logger.LogInformation("Adding {quantity} events to calendar", events.Count());

        foreach (var @event in events)
        {
            var icalEvent = new CalendarEvent
            {
                Start = new CalDateTime(@event.Start),
                End = new CalDateTime(@event.End),
                Summary = @event.Name,
                Description = @event.Description
            };

            calendar.Events.Add(icalEvent);
        }

        var serializer = new CalendarSerializer();
        var serializedCalendar = serializer.SerializeToString(calendar);

        logger.LogDebug("Serialized calendar: {serializedCalendar}", serializedCalendar);

        var path = Path.Combine(Directory.GetCurrentDirectory(), "week.ics");
        await File.WriteAllTextAsync(path, serializedCalendar);

        logger.LogInformation("Week file created");
    }
}