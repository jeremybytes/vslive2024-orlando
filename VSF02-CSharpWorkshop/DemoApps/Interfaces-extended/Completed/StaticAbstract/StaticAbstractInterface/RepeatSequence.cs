namespace StaticAbstract;

// RepeatSequence taken from tutorial
// on static abstract interface members
// Tutorial: Explore C# 11 feature - static
// virtual members in interfaces
// https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/static-virtual-interface-members

public record RepeatSequence : IGetNext<RepeatSequence>
{
    private const char Ch = 'A';
    public string Text = new string(Ch, 1);

    public static RepeatSequence operator ++(RepeatSequence other)
    {
        return other with { Text = other.Text + Ch };
    }

    public override string ToString() => Text;
}
