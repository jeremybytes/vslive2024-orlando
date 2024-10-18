namespace HouseControl.Library;

public class CurrentTimeProvider : ITimeProvider
{
    public DateTimeOffset Now()
    {
        return DateTimeOffset.Now;
    }
}
