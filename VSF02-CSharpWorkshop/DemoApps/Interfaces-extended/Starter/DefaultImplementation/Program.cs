namespace DefaultImplementation;

class Program
{
    static void Main(string[] args)
    {
        ILogger logger = new InitialLogger();
        logger.Log(LogLevel.Warning, "Just a warning. Carry on.");

        try
        {
            throw new NotImplementedException("There is no implementation.");
        }
        catch (NotImplementedException ex)
        {
            logger.Log(LogLevel.Error, ex.Message);
        }

        Console.ReadLine();
    }
}