namespace HouseControl.Library;

public interface ITimeProvider
{
    DateTimeOffset Now();
}
