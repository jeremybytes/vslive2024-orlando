namespace DefaultImplementation;

public class InitialLogger : ILogger
{
    public void Log(LogLevel level, string message)
    {
        Console.WriteLine($"Level - {level:F}: {message}");
    }
}
