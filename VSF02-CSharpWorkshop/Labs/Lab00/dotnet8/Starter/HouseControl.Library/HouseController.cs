using System.Timers;

namespace HouseControl.Library;

public class HouseController
{
    private SerialCommander commander;

    private System.Timers.Timer scheduler = new(60000);
    private Schedule schedule;

    public HouseController(Schedule schedule)
    {
        commander = new SerialCommander();

        this.schedule = schedule;

        scheduler.Elapsed += scheduler_Elapsed;
        scheduler.AutoReset = true;
        scheduler.Start();
    }

    private async void scheduler_Elapsed(object? sender, ElapsedEventArgs e)
    {
        var itemsToProcess = schedule.GetCurrentScheduleItems();

        foreach (var item in itemsToProcess)
            await SendCommand(item.Device, item.Command);

#if DEBUG
        Console.Write($"Schedule Items Processed: {itemsToProcess.Count()} - ");
#endif

        schedule.RollSchedule();

#if DEBUG
        Console.WriteLine($"Total Items: {schedule.Count} - Active Items: {schedule.Count(si => si.IsEnabled).ToString()}");
#endif
    }

    public async Task ResetAll()
    {
        for (int i = 1; i <= 8; i++)
        {
            await SendCommand(i, DeviceCommand.Off);
        }
    }

    public void ScheduleOneTimeItem(DateTimeOffset time, int device,
        DeviceCommand command)
    {
        var scheduleItem = new ScheduleItem(
            device,
            command,
            new ScheduleInfo()
            {
                EventTime = time,
                Type = ScheduleType.Once,
            },
            true,
            ""
        );
        schedule.Add(scheduleItem);
    }

    public async Task SendCommand(int device, DeviceCommand command)
    {
        await commander.SendCommand(device, command);
        Console.WriteLine($"{DateTime.Now:G} - Device: {device}, Command: {command}");
    }

    public List<ScheduleItem> GetCurrentScheduleItems()
    {
        return schedule.Where(i => i.IsEnabled)
            .OrderBy(i => i.Info.EventTime).ToList();
    }

    public void ReloadSchedule()
    {
        schedule.LoadSchedule();
    }

    public void SaveSchedule()
    {
        schedule.SaveSchedule();
    }
}
