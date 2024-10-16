using Microsoft.EntityFrameworkCore;
using PersonReader.Interface;

namespace PersonReader.SQL;

public class SQLReader : IPersonReader, IDisposable
{
    PersonContext context;

    public SQLReader()
    {
        string sqlFileName = $"{AppDomain.CurrentDomain.BaseDirectory}People.db";
        var optionsBuilder = new DbContextOptionsBuilder<PersonContext>();
        optionsBuilder.UseSqlite($"Data Source={sqlFileName}");
        var options = optionsBuilder.Options;
        context = new PersonContext(options);
    }

    public async Task<IReadOnlyCollection<Person>> GetPeople()
    {
        return await context.People!.OrderBy(p => p.StartDate).ToListAsync();
    }

    public async Task<Person?> GetPerson(int id)
    {
        return await context.People!.FirstOrDefaultAsync(p => p.Id == id);
    }

    #region IDisposable Support
    private bool disposedValue = false; // To detect redundant calls

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                context.Dispose();
            }
            disposedValue = true;
        }
    }

    // This code added to correctly implement the disposable pattern.
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        Dispose(true);
    }
    #endregion
}

