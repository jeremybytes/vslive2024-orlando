namespace StaticAbstract;

// Interface taken from tutorial
// on static abstract interface members
// Tutorial: Explore C# 11 feature - static
// virtual members in interfaces
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members

public interface IGetNext<T> where T : IGetNext<T>
{
    static abstract T operator ++(T other);
}
