using Innovative.SolarCalculator;

namespace SolarCalculator.Service.Models;

// SAMPLE DATA
//{
//    "results": {
//        "sunrise":"6:39:53 AM",
//        "sunset":"6:48:32 PM",
//        "solar_noon":"12:44:12 PM",
//        "day_length":"12:08:38.8300000" },
//    "status":"OK"
//}

#pragma warning disable IDE1006 // Naming Styles
public record Results(string sunrise, string sunset, string solar_noon, string day_length);
public record SolarData(Results? results, string status);
#pragma warning restore IDE1006 // Naming Styles

public class SolarCalculatorProvider
{
    public static SolarData GetSolarTimes(
        DateTime date, double latitude, double longitude)
    {
        SolarData result;

        try
        {
            var solarTimes = new SolarTimes(date, latitude, longitude);
            result = new(
                new(
                    solarTimes.Sunrise.ToLongTimeString(),
                    solarTimes.Sunset.ToLongTimeString(),
                    solarTimes.SolarNoon.ToLongTimeString(),
                    solarTimes.SunlightDuration.ToString()
                    ),
                "OK");
        }
        catch (Exception)
        {
            result = new(null, "ERROR");
        }
        return result;
    }
}