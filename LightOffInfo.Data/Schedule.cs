namespace LightOffInfo.Data;

public class Schedule
{
    public string Name { get; set; }
    public IEnumerable<(TimeSpan, TimeSpan)> Offline {get;set; }
    public Condition Condition { get; set; }
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
