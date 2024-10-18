using System.Text.Json;

namespace HouseControl.Library;

public class JsonLoader : IScheduleLoader
{
    public IEnumerable<ScheduleItem> LoadScheduleItems(string filename)
    {
        var output = new List<ScheduleItem>();
        filename = filename + ".json";

        if (File.Exists(filename))
        {
            using var reader = new StreamReader(filename);
            output = JsonSerializer.Deserialize<List<ScheduleItem>>(
                reader.ReadToEnd());
        }

        return output!;
    }
}
