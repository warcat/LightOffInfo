using System.Security.Cryptography.X509Certificates;
using LightOffInfo.Data;
using Majorsoft.Blazor.Extensions.BrowserStorage;

internal class LocalStorageSettings
{
	private readonly ILocalStorageService _localStorage;

	public LocalStorageSettings(ILocalStorageService localStorage)
	{
		_localStorage = localStorage;
	}

	public async Task WriteCurrentLocation(Location location)
	{
		await _localStorage.SetItemAsync("CurrentLocation", location);
        await _localStorage.SetItemAsync("CurrentLocation_Schedules", location.Schedules);
	}

    public async Task<Location> ReadCurrentLocation()
    {
		var location = await _localStorage.GetItemAsync<Location>("CurrentLocation");
		location.Schedules = await _localStorage.GetItemAsync<Schedule[]>("CurrentLocation_Schedules");

		return location;
    }
}