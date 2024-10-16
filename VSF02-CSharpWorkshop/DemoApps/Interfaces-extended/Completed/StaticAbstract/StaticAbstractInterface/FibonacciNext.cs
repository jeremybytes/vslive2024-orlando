namespace StaticAbstract;

// FibonacciNext is custom code based on
// a tutorial on static abstract interface members
// Tutorial: Explore C# 11 feature - static
// virtual members in interfaces
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members

public record FibonacciNext : IGetNext<FibonacciNext>
{
    public int Previous = 0;
    public int Current = 1;

    public static FibonacciNext operator ++(FibonacciNext other)
    {
        checked
        {
            int next = other.Previous + other.Current;
            return other with { Previous = other.Current, Current = next };
        }
    }

    public override string ToString() => Current.ToString();
}
