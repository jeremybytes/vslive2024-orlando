using HouseControl.Sunset;

namespace HouseControl.Library.Test;

public class FakeSunsetProvider : ISunsetProvider
{
    public DateTimeOffset GetSunrise(DateTime date)
    {
        DateTime time = DateTime.Parse("06:15:00");
        return new DateTimeOffset(date.Date + time.TimeOfDay);
    }

    public DateTimeOffset GetSunset(DateTime date)
    {
        DateTime time = DateTime.Parse("20:25:00");
        return new DateTimeOffset(date.Date + time.TimeOfDay);
    }
}
