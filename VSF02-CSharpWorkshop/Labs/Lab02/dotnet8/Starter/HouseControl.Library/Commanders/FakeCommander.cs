namespace HouseControl.Library;

public class FakeCommander : ICommander
{
    public Task SendCommand(int deviceNumber, DeviceCommand command)
    {
        Console.WriteLine($"Device {deviceNumber}: {command}");
        return Task.CompletedTask;
    }
}
