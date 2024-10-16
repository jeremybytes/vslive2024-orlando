namespace StaticMembers;

public class CSVPeopleReader : IPeopleReader
{
    public IReadOnlyCollection<Person> GetPeople()
    {
        var people = ParseString(IFileLoader.FileData);
        return people;
    }

    public Person GetPerson(int id)
    {
        var person = GetPeople().FirstOrDefault(p => p.ID == id);
        return person ?? new Person(0, "", "", DateTime.Now, 0, "");
    }

    private List<Person> ParseString(string csvData)
    {
        var people = new List<Person>();

        var lines = csvData.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        foreach (string line in lines)
        {
            try
            {
                var elems = line.Split(',');
                var per = new Person(
                    Int32.Parse(elems[0]),
                    elems[1],
                    elems[2],
                    DateTime.Parse(elems[3]),
                    Int32.Parse(elems[4]),
                    elems[5]
                );
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
