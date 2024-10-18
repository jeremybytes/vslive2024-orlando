using Innovative.SolarCalculator;

namespace HouseControl.Sunset;

public class SolarTimesSunsetProvider : ISunsetProvider
{
    private readonly double latitude;
    private readonly double longitude;

    public SolarTimesSunsetProvider(double latitude, double longitude)
    {
        this.latitude = latitude;
        this.longitude = longitude;
    }

    public DateTimeOffset GetSunrise(DateTime date)
    {
        var solarTimes = new SolarTimes(date, latitude, longitude);
        return new DateTimeOffset(solarTimes.Sunrise);
    }

    public DateTimeOffset GetSunset(DateTime date)
    {
        var solarTimes = new SolarTimes(date, latitude, longitude);
        return new DateTimeOffset(solarTimes.Sunset);
    }
}
