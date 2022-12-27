namespace LightOffInfo.Data;

public class Schedule
{
    public bool? IsExpanded { get; set; }
    public string LocationName { get; set; }

    public string Name { get; set; }
    public Offline[] Offline { get; set; }
    public Condition Condition { get; set; }
}

public class Offline
{
    public TimeSpan From { get; set; }
    public TimeSpan To { get; set; }

    public Offline(TimeSpan from, TimeSpan to)
    {
        From = from;
        To = to;
    }
}

public class Condition
{
    public IEnumerable<DayOfWeek> DaysOfWeek { get; set; }
    public IEnumerable<int> DaysOfMonth { get; set; }

    public Condition()
    {
        DaysOfWeek = Enumerable.Empty<DayOfWeek>();
        DaysOfMonth = Enumerable.Empty<int>();
    }

    public bool IsOfflineOnDate(DateTime d)
    {
        if (DaysOfWeek.Any())
            return DaysOfWeek.Contains(d.DayOfWeek);

        return DaysOfMonth.Contains(d.Day);
    }
}
