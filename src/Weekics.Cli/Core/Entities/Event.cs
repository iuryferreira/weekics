namespace Weekics.Cli.Core.Entities
{
    public class Event
    {
        public required string Name { get; set; }
        public required DateOnly Date { get; set; }
        public required TimeOnly StartAt { get; set; }
        public required TimeOnly EndAt { get; set; }
        public string? Description { get; set; }

        public DateTime Start => new(Date.Year, Date.Month, Date.Day, StartAt.Hour, StartAt.Minute, StartAt.Second);
        public DateTime End => new(Date.Year, Date.Month, Date.Day, EndAt.Hour, EndAt.Minute, EndAt.Second);

    }
}