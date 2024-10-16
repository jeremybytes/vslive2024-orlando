namespace StaticAbstract;

internal class Program
{
    static void Main(string[] args)
    {
        var repeatNexter = new Nexter<RepeatSequence>();
        int count = 0;
        foreach(var s in repeatNexter)
        {
            if (++count > 10) break;
            Console.WriteLine(s);
        }

        var fibNexter = new Nexter<FibonacciNext>();
        foreach (var s in fibNexter)
        {
            Console.WriteLine(s);
        }
    }
}