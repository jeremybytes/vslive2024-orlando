namespace HouseControl.Sunset;

public interface ISunsetGetter
{
    Task<DateTimeOffset> GetSunset(DateOnly date);
    Task<DateTimeOffset> GetSunrise(DateOnly date);
}
