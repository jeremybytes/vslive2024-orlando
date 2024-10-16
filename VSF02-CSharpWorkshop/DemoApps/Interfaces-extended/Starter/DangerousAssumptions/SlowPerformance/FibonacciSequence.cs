using System.Collections;

namespace DangerousAssumptions.SlowPerformance;

public class FibonacciSequence : IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        var value = (current: 0, next: 1);
        Console.WriteLine();

        bool notOverflowed = true;
        while (notOverflowed)
        {
            Thread.Sleep(100);
            value = (value.next, value.current + value.next);
            if (value.next < 0)
            {
                // this denotes that the integer field
                // has overflowed.
                notOverflowed = false;
            }
            Console.Write($"{value.current} ");
            yield return value.current;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
