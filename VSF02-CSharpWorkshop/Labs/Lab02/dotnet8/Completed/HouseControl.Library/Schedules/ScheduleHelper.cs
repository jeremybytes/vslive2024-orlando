using HouseControl.Sunset;

namespace HouseControl.Library;

public class ScheduleHelper
{
    private readonly ISunsetProvider SunsetProvider;

    public ScheduleHelper(ISunsetProvider sunsetProvider)
    {
        this.SunsetProvider = sunsetProvider;
    }

    private static ITimeProvider? timeProvider;
    public static ITimeProvider TimeProvider
    {
        get => timeProvider ??= new CurrentTimeProvider();
        set => timeProvider = value;
    }


    public static DateTimeOffset Now()
    {
        return TimeProvider.Now();
    }

    public static DateTimeOffset Today()
    {
        return new DateTimeOffset(Now().Date, Now().Offset);
    }

    public static DateTimeOffset Tomorrow()
    {
        return new DateTimeOffset(
            Now().Date.AddDays(1),
            Now().Offset);
    }

    public static bool IsInPast(DateTimeOffset checkTime)
    {
        return checkTime < Now();
    }

    public static bool IsInFuture(DateTimeOffset checkTime)
    {
        return !IsInPast(checkTime);
    }

    public static TimeSpan DurationFromNow(DateTimeOffset checkTime)
    {
        return (checkTime - Now()).Duration();
    }

    public DateTimeOffset RollForwardToNextDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Tomorrow();
        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }

    public DateTimeOffset RollForwardToNextWeekdayDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Tomorrow();
        while (nextDay.DayOfWeek == DayOfWeek.Saturday
            || nextDay.DayOfWeek == DayOfWeek.Sunday)
        {
            nextDay = nextDay.AddDays(1);
        }

        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }

    public DateTimeOffset RollForwardToNextWeekendDay(ScheduleInfo info)
    {
        if (IsInFuture(info.EventTime))
            return info.EventTime;

        var nextDay = Tomorrow();
        while (nextDay.DayOfWeek != DayOfWeek.Saturday
            && nextDay.DayOfWeek != DayOfWeek.Sunday)
        {
            nextDay = nextDay.AddDays(1);
        }
        return info.TimeType switch
        {
            ScheduleTimeType.Standard => nextDay + info.EventTime.TimeOfDay + info.RelativeOffset,
            ScheduleTimeType.Sunset => SunsetProvider.GetSunset(nextDay.Date) + info.RelativeOffset,
            ScheduleTimeType.Sunrise => SunsetProvider.GetSunrise(nextDay.Date) + info.RelativeOffset,
            _ => info.EventTime
        };
    }
}
