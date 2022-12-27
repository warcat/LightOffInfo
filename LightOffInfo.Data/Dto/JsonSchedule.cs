namespace LightOffInfo.Data.Dto;

public class JsonSchedule
{
    public string name { get; set; }
    public string offline { get; set; }
    public JsonCondition condition { get; set; }

    public Schedule ToSchedule(Location location) => new Schedule
    {
        Name = name,
        Condition = condition.ToCondition(),
        Offline = ParseOfflineTime().ToArray(),
        LocationName = location.Name
    };

    private IEnumerable<Offline> ParseOfflineTime()
    {
        if (string.IsNullOrEmpty(offline))
            throw new ArgumentNullException("offline");

        var groups = offline.Split(',');
        foreach (var group in groups)
        {
            var hours = group.Split('-');
            if (hours.Length != 2)
                continue;

            var t1 = hours[0].Contains(':') ? TimeSpan.Parse(hours[0]) : TimeSpan.FromHours(int.Parse(hours[0]));
            var t2 = hours[1].Contains(':') ? TimeSpan.Parse(hours[1]) : TimeSpan.FromHours(int.Parse(hours[1]));

            yield return new Offline(t1, t2);
        }
    }
}

public class JsonCondition
{
    public string? daysOfWeek { get; set; }
    public string? daysOfMonth { get; set; }

    public Condition ToCondition()
    {
        if (daysOfWeek != null)
            return new Condition
            {
                DaysOfWeek = ParseInts(daysOfWeek).Select(x => (DayOfWeek)x).ToArray()
            };

        if (daysOfMonth != null)
            return new Condition
            {
                DaysOfMonth = ParseInts(daysOfMonth).ToArray()
            };

        throw new NotSupportedException("Condition type is not supported");
    }

    private static IEnumerable<int> ParseInts(string strArray)
    {
        return strArray.Split(',').Select(x => int.Parse(x));
    }
}
