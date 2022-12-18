using System.Net.Http.Json;
using System.Text.Json.Serialization;
using LightOffInfo.Data.Dto;

namespace LightOffInfo.Data;

public class Locations
{
    private readonly string _listUrl;

    public Locations(string listUrl)
    {
        _listUrl = listUrl;
    }

    public async Task<Location[]> ReadLocations(HttpClient http)
    {
        return await http.GetFromJsonAsync<Location[]>(_listUrl) ?? Array.Empty<Location>();
    }
}

public class Location
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("schedule_url")]
    public string ScheduleUrl { get; set; }

    public IEnumerable<Schedule> Schedules { get; private set; }

    public async Task ReadSchedule(HttpClient http)
    {
        var data = await http.GetFromJsonAsync<JsonSchedule[]>(ScheduleUrl);
        if (data == null)
            throw new KeyNotFoundException("Schedules are not found at " + ScheduleUrl);

        Schedules = data.Select(x => x.ToSchedule()).ToArray();
    }

    public IEnumerable<Schedule> GetSchedulesOnDate(DateTime d)
    {
        return Schedules.Where(s => s.Condition.IsOfflineOnDate(d));
    }
}