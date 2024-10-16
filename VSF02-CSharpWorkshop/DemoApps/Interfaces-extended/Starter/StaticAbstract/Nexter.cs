using System.Collections;

namespace StaticAbstract;

// Nexter<T> uses the IGetNext<T> interface to
// add enumarable functionality to IGetNext (which
// includes a ++ operator).

public class Nexter<T> : IEnumerable<T> where T : IGetNext<T>, new()
{
    public IEnumerator<T> GetEnumerator()
    {
        // first value
        T value = new();
        yield return value;

        // subsequent values
        while (true)
        {
            try { value++; }
            catch { break; }
            yield return value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
