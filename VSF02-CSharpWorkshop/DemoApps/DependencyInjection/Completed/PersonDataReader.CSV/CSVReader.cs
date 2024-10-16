using PeopleViewer.Common;

namespace PersonDataReader.CSV;

public class CSVReader : IPersonReader
{
    private string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}People.txt";

    // START Code Block #1: "Simple" Property Injection
    // Dependency is "new"ed up by the constructor every time
    // (if the property is overridden before any method calls,
    // the default is still "new"ed up even though it is never used).

    public ICSVFileLoader FileLoader { get; set; }

    public CSVReader()
    {
        FileLoader = new CSVFileLoader(filePath);
    }

    // END Code Block #1

    // START Code Block #2: "Safe" Property Injection
    // Dependency is not "new"ed up until after it is asked for
    // (if the property is overridden before any method calls,
    // the default is never "new"ed up).

    //private ICSVFileLoader? fileLoader;
    //public ICSVFileLoader FileLoader
    //{
    //    get { return fileLoader ??= new CSVFileLoader(filePath); }
    //    set { fileLoader = value; }
    //}

    // END Code Block #2

    public async Task<IReadOnlyCollection<Person>> GetPeople()
    {
        var fileData = await FileLoader.LoadFile();
        var people = ParseData(fileData);
        return people;
    }

    public async Task<Person?> GetPerson(int id)
    {
        var people = await GetPeople();
        return people.FirstOrDefault(p => p.Id == id);
    }

    private List<Person> ParseData(IReadOnlyCollection<string> csvData)
    {
        var people = new List<Person>();

        foreach (string line in csvData)
        {
            try
            {
                var elems = line.Split(',');
                var per = new Person()
                {
                    Id = Int32.Parse(elems[0]),
                    GivenName = elems[1],
                    FamilyName = elems[2],
                    StartDate = DateTime.Parse(elems[3]),
                    Rating = Int32.Parse(elems[4]),
                    FormatString = elems[5],
                };
                people.Add(per);
            }
            catch (Exception)
            {
                // Skip the bad record, log it, and move to the next record
                // log.write("Unable to parse record", per);
            }
        }
        return people;
    }
}
