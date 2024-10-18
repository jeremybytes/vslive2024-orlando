using System.Text.Json;

namespace HouseControl.Sunset;

public class SolarServiceSunsetProvider
{
#pragma warning disable IDE1006 // Naming Styles
    private record Results(string sunrise, string sunset, string solar_noon, string day_length);
    private record SolarData(Results results, string status);
#pragma warning restore IDE1006 // Naming Styles

    private readonly double latitude;
    private readonly double longitude;

    public SolarServiceSunsetProvider(double latitude, double longitude)
    {
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public DateTimeOffset GetSunrise(DateTime date)
    {
        string serviceData = GetServiceData(date, latitude, longitude);
        string sunriseTimeString = ParseSunriseTime(serviceData);
        DateTime sunriseTime = ToLocalTime(sunriseTimeString, date);
        return new DateTimeOffset(sunriseTime);
    }

    public DateTimeOffset GetSunset(DateTime date)
    {
        string serviceData = GetServiceData(date, latitude, longitude);
        string sunsetTimeString = ParseSunsetTime(serviceData);
        DateTime sunsetTime = ToLocalTime(sunsetTimeString, date);
        return new DateTimeOffset(sunsetTime);
    }

    public static DateTime ToLocalTime(string inputTime, DateTime date)
    {
        DateTime time = DateTime.Parse(inputTime);
        DateTime result = date.Date + time.TimeOfDay;
        return result;
    }

    public static string ParseSunsetTime(string jsonData)
    {
        SolarData? data = JsonSerializer.Deserialize<SolarData>(jsonData);
        string sunsetTimeString = data!.results.sunset;
        return sunsetTimeString;
    }

    public static string ParseSunriseTime(string jsonData)
    {
        SolarData? data = JsonSerializer.Deserialize<SolarData>(jsonData);
        return data!.results.sunrise;
    }

    private static readonly HttpClient client =
        new() { BaseAddress = new Uri("http://localhost:8973") };

    private string GetServiceData(DateTime date, double latitude, double longitude)
    {
        HttpResponseMessage response =
            client.GetAsync($"SolarCalculator/{latitude:F4}/{longitude:F4}/{date:yyyy-MM-dd}").Result;

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Unable to complete request: status code {response.StatusCode}");

        var stringResult =
            response.Content.ReadAsStringAsync().Result;

        return stringResult;
    }
}
