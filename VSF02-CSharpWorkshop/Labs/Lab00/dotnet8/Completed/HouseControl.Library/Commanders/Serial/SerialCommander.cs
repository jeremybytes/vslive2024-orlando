using System.Configuration;
using System.IO.Ports;

namespace HouseControl.Library;

public class SerialCommander
{
    private SerialPort serialPort;
    private int baud = 115200;
    private int databits = 8;
    private Parity parity = Parity.None;
    private StopBits stopbits = StopBits.One;

    public SerialCommander()
    {
        string port = ConfigurationManager.AppSettings["ComPort"] ?? "COM5";
        serialPort = new SerialPort(port, baud, parity, databits, stopbits);
    }

    public async Task SendCommand(int deviceNumber, DeviceCommand command)
    {
        string message = MessageGenerator.GetMessage(deviceNumber, command);
        serialPort.Open();
        serialPort.DtrEnable = true;
        serialPort.RtsEnable = true;

        foreach (var bit in message)
        {
            switch (bit)
            {
                case '0':
                    serialPort.RtsEnable = false;
                    await Task.Delay(10);
                    serialPort.RtsEnable = true;
                    await Task.Delay(10);
                    break;
                case '1':
                    serialPort.DtrEnable = false;
                    await Task.Delay(10);
                    serialPort.DtrEnable = true;
                    await Task.Delay(10);
                    break;
                default:
                    throw new ArgumentException(
                        "Message can only contain '1' and '0' characters");
            }
        }

        await Task.Delay(50);
        serialPort.RtsEnable = false;
        serialPort.DtrEnable = false;
        serialPort.Close();
    }
}
