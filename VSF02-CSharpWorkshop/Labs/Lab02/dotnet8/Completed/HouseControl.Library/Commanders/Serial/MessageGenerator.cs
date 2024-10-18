namespace HouseControl.Library;

internal static class MessageGenerator
{
    private static string header = "11010101" + "10101010";
    private static string codeA = "01100000";
    private static string footer = "10101101";

    private static List<string> onCommands = new List<string>
    {
        "", // dummy entry
        "00000000", // 1 on
        "00010000", // 2 on
        "00001000", // 3 on
        "00011000", // 4 on
        "01000000", // 5 on
        "01010000", // 6 on
        "01001000", // 7 on
        "01011000", // 8 on
    };

    private static List<string> offCommands = new List<string>
    {
        "", // dummy entry
        "00100000", // 1 off
        "00110000", // 2 off
        "00101000", // 3 off
        "00111000", // 4 off
        "01100000", // 5 off
        "01110000", // 6 off
        "01101000", // 7 off
        "01111000", // 8 off
    };

    //private static string bright = "10001000";
    //private static string dim = "10011000";

    public static string GetMessage(int deviceNumber, DeviceCommand command)
    {
        if (deviceNumber < 1 || deviceNumber > 8)
            throw new ArgumentException("Invalid Device Requested");

        string message = header + codeA;

        message += command switch
        {
            DeviceCommand.On => onCommands[deviceNumber],
            DeviceCommand.Off => offCommands[deviceNumber],
            _ => throw new ArgumentException("Invalid Command Requested"),
        };
        message += footer;
        return message;
    }

}
