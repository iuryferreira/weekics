using Weekics.Cli;
using Weekics.Cli.Core;

Container.Build();

var path = args[0];

var eventService = Container.GetService<IEventService>();
var icsService = Container.GetService<IIcsService>();

var events = await eventService.GetEventsAsync(path);
await icsService.CreateWeekFile(events);

