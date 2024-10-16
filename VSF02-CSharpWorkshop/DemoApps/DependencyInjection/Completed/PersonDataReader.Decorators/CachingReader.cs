using PeopleViewer.Common;

namespace PersonDataReader.Decorators;

public class CachingReader : IPersonReader
{
    private IPersonReader _wrappedReader;
    private TimeSpan _cacheDuration = new TimeSpan(0, 0, 30);

    private IReadOnlyCollection<Person> _cachedItems = new List<Person>();
    private DateTime _dataDateTime;

    public CachingReader(IPersonReader wrappedReader)
    {
        _wrappedReader = wrappedReader;
    }

    public async Task<IReadOnlyCollection<Person>> GetPeople()
    {
        await ValidateCache();
        return _cachedItems;
    }

    public async Task<Person?> GetPerson(int id)
    {
        await ValidateCache();
        return _cachedItems.FirstOrDefault(p => p.Id == id);
    }

    private bool IsCacheValid
    {
        get
        {
            if (_cachedItems == null)
                return false;
            var _cacheAge = DateTime.Now - _dataDateTime;
            return _cacheAge < _cacheDuration;
        }
    }

    private async Task ValidateCache()
    {
        if (IsCacheValid)
            return;

        try
        {
            _cachedItems = await _wrappedReader.GetPeople();
            _dataDateTime = DateTime.Now;
        }
        catch
        {
            _cachedItems = new List<Person>()
                {
                    new Person()
                    {
                        GivenName = "No Data Available",
                        FamilyName = string.Empty,
                        Rating = 0,
                        StartDate = DateTime.Today,
                    }
                };
            InvalidateCache();
        }
    }

    private void InvalidateCache()
    {
        _dataDateTime = DateTime.MinValue;
    }

    public string GetTypeName()
    {
        return $"{this.GetType().Name} ({_wrappedReader.GetTypeName()})";
    }
}
