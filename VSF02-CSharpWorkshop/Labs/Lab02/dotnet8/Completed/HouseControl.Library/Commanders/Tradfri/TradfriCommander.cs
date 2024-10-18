namespace HouseControl.Library;

public class TradfriCommander
{
    HttpClient client = new();

    public TradfriCommander(Uri uri)
    {
        client.BaseAddress = uri;
    }

    public async Task SendCommand(int deviceNumber, DeviceCommand command)
    {
        string queryString = 
            $"deviceNumber={deviceNumber}&command={command}&level=100";
        HttpResponseMessage response = await client.GetAsync($"tradfri/?{queryString}");
        if (!response.IsSuccessStatusCode)
        {
            // unsuccessful
        }
    }
}
