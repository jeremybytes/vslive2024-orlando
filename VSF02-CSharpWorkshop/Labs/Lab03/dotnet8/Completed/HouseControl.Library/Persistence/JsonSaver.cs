using System.Text.Json;

namespace HouseControl.Library;

public class JsonSaver : IScheduleSaver
{
    public void SaveScheduleItems(string filename, IEnumerable<ScheduleItem> schedule)
    {
        filename = filename + ".json";
        var output = JsonSerializer.Serialize(schedule, new JsonSerializerOptions() { WriteIndented = true });

        using var writer = new StreamWriter(filename, false);
        writer.WriteLine(output);
    }
}
