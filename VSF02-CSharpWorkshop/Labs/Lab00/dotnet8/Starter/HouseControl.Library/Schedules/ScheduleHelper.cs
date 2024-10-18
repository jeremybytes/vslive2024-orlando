using HouseControl.Sunset;

namespace HouseControl.Library;

public class ScheduleHelper
{
    private readonly SolarServiceSunsetProvider SunsetProvider;

    public ScheduleHelper(SolarServiceSunsetProvider sunsetProvider)
    {
        this.SunsetProvider = sunsetProvider;
    }

    public static DateTimeOffset Tomorrow()
    {
        return new DateTimeOffset(
            DateTimeOffset.Now.Date.AddDays(1),
            DateTimeOffset.Now.Offset);
    }

    public static bool IsInFuture(DateTimeOffset checkTime)
    {
        return checkTime > DateTimeOffset.Now;
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
