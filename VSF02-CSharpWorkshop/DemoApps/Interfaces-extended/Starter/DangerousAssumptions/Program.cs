using DangerousAssumptions.SlowPerformance;

class Program
{
    static void Main(string[] args)
    {
        IReader<int> fibReader = new FibonacciReader();
        Console.WriteLine("Get All Items:");
        foreach (var _ in fibReader.GetItems()) 
        {
            // force an iteration of all items 
            // (the implementation code prints
            //  the current value to the console)
        }
        Console.WriteLine("\n----------------------------------");

        int index = 5;
        Console.WriteLine($"\nGet Item At ({index}):");
        Console.WriteLine($"\nNumber at index {index}: {fibReader.GetItemAt(index)}");
        Console.WriteLine("----------------------------------");
        Console.WriteLine();

        Console.ReadLine();
    }
}