namespace HouseControl.Sunset;

public interface ISunsetProvider
{
    DateTimeOffset GetSunrise(DateTime date);
    DateTimeOffset GetSunset(DateTime date);
}
