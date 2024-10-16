using HouseControl.Sunset;

namespace SunsetTestApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        //Console.WriteLine("This doesn't do anything yet");
        //await Task.Delay(1);

        ISunsetGetter provider = new ServiceSunsetGetter();
        var today = DateOnly.FromDateTime(DateTime.Today);
        var sunrise = await provider.GetSunrise(today);
        var sunset = await provider.GetSunset(today);
        Console.WriteLine("Today's Sunrise/Sunset Times:");
        Console.WriteLine($"Date: {today}");
        Console.WriteLine($"Sunrise: {sunrise}");
        Console.WriteLine($"Sunset: {sunset}");
    }
}
