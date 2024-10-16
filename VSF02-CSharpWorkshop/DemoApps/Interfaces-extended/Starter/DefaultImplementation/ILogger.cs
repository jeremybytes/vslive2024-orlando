namespace DefaultImplementation;

public enum LogLevel
{
    None,
    Info,
    Warning,
    Error,
}

public interface ILogger
{
    void Log(LogLevel level, string message);
}
