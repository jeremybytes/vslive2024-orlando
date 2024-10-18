namespace HouseControl.Library;

public interface ICommander
{
    Task SendCommand(int deviceNumber, DeviceCommand command);
}
