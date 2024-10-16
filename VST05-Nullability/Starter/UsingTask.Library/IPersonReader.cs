using UsingTask.Shared;

namespace UsingTask.Library
{
    public interface IPersonReader
    {
        Task<List<Person>> GetAsync(CancellationToken cancelToken = default);
    }
}