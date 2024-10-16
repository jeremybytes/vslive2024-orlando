using PeopleViewer.Common;

namespace PeopleViewer.Presentation.Tests;

public class FakeReader : IPersonReader
{
    private List<Person> testData = new List<Person>()
    {
        new Person() {Id = 1,
            GivenName = "John", FamilyName = "Smith",
            Rating = 7, StartDate = new DateTime(2000, 10, 1)},
        new Person() {Id = 2,
            GivenName = "Mary", FamilyName = "Thomas",
            Rating = 9, StartDate = new DateTime(1971, 7, 23)},
    };

    public async Task<IReadOnlyCollection<Person>> GetPeople()
    {
        await Task.Delay(1);
        return testData;
    }

    public async Task<Person?> GetPerson(int id)
    {
        await Task.Delay(1);
        return testData.First(p => p.Id == id);
    }
}
