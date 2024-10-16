namespace DefaultImplementation;

    public class JeremyLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            Console.WriteLine($"JeremyLogger Level - {level:F}: {message}");
        }

        public void LogException(Exception ex)
        {
            Log(LogLevel.Error, $"From JeremyLogger: {ex.Message}");
        }
    }


