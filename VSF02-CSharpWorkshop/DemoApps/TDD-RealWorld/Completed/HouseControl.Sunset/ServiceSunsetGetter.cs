using System.Text.Json;

namespace HouseControl.Sunset;

public class ServiceSunsetGetter : ISunsetGetter
{
    private record Results(string sunrise, string sunset, string solar_noon, string day_length);
    private record SolarData(Results results, string status);

    private ISolarService? service;

    public ISolarService Service
    {
        get => service ??= new SolarService();
        set => service = value;
    }

    public async Task<DateTimeOffset> GetSunrise(DateOnly date)
    {
        string jsonData = await Service.GetServiceData(date);
        string sunriseTimeString = ParseSunriseTime(jsonData);
        DateTimeOffset result = ToLocalTime(date, sunriseTimeString);
        return result;
    }

    public async Task<DateTimeOffset> GetSunset(DateOnly date)
    {
        string jsonData = await Service.GetServiceData(date);
        string sunsetTimeString = ParseSunsetTime(jsonData);
        DateTimeOffset result = ToLocalTime(date, sunsetTimeString);
        return result;
    }

    public bool CheckStatus(string jsonData)
    {
        SolarData? data = JsonSerializer.Deserialize<SolarData>(jsonData);
        return data!.status == "OK";
    }

    public string ParseSunsetTime(string jsonData)
    {
        if (!CheckStatus(jsonData))
            throw new ArgumentException(jsonData);

        SolarData? data = JsonSerializer.Deserialize<SolarData>(jsonData);
        return data!.results.sunset;
    }

    public DateTimeOffset ToLocalTime(DateOnly date, string timeString)
    {
        TimeOnly time = TimeOnly.Parse(timeString);
        DateTimeOffset result = date.ToDateTime(time);
        return result;
    }

    public string ParseSunriseTime(string jsonData)
    {
        if (!CheckStatus(jsonData))
            throw new ArgumentException(jsonData);

        SolarData? data = JsonSerializer.Deserialize<SolarData>(jsonData);
        return data!.results.sunrise;
    }
}
