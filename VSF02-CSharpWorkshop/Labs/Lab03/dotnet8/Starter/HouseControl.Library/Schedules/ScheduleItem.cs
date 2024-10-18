namespace HouseControl.Library;

public record ScheduleItem(int Device, DeviceCommand Command, ScheduleInfo Info, 
    bool IsEnabled, string ScheduleSet);

public class ScheduleInfo
{
    public DateTimeOffset EventTime { get; set; }
    public ScheduleTimeType TimeType { get; set; }
    public TimeSpan RelativeOffset { get; set; }
    public ScheduleType Type { get; set; }
}
