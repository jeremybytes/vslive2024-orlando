using HouseControl.Sunset;

namespace HouseControl.Library;

public class Schedule : List<ScheduleItem>
{
    private string filename;

    private IScheduleLoader? loader;
    public IScheduleLoader Loader
    {
        get => loader ??= new JsonLoader();
        set => loader = value;
    }

    private IScheduleSaver? saver;
    public IScheduleSaver Saver
    {
        get => saver ??= new JsonSaver();
        set => saver = value;
    }

    private readonly ScheduleHelper scheduleHelper;

    public Schedule(string filename, SolarServiceSunsetProvider sunsetProvider)
    {
        this.scheduleHelper = new ScheduleHelper(sunsetProvider);
        this.filename = filename;
        LoadSchedule();
    }

    public void LoadSchedule()
    {
        this.Clear();
        this.AddRange(Loader.LoadScheduleItems(filename));

        // update loaded schedule dates to today
        DateTimeOffset today = DateTimeOffset.Now.Date;
        foreach (var item in this)
        {
            item.Info.EventTime = today + item.Info.EventTime.TimeOfDay;
        }
        RollSchedule();
    }

    public void SaveSchedule()
    {
        Saver.SaveScheduleItems(filename, this);
    }

    public List<ScheduleItem> GetCurrentScheduleItems()
    {
        return this.Where(si => si.IsEnabled &&
            (si.Info.EventTime - DateTimeOffset.Now).Duration() < TimeSpan.FromSeconds(30))
            .ToList();
    }

    public void RollSchedule()
    {
        for (int i = Count - 1; i >= 0; i--)
        {
            var currentItem = this[i];
            while (currentItem.Info.EventTime < DateTimeOffset.Now)
            {
                if (currentItem.Info.Type == ScheduleType.Once)
                {
                    this.RemoveAt(i);
                    break;
                }

                currentItem.Info.EventTime =
                    currentItem.Info.Type switch
                    {
                        ScheduleType.Daily => scheduleHelper.RollForwardToNextDay(currentItem.Info),
                        ScheduleType.Weekday => scheduleHelper.RollForwardToNextWeekdayDay(currentItem.Info),
                        ScheduleType.Weekend => scheduleHelper.RollForwardToNextWeekendDay(currentItem.Info),
                        _ => currentItem.Info.EventTime
                    };
            }
        }
    }

}
