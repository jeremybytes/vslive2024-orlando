namespace People.Service;

public interface IPeopleProvider
{
    List<Person> GetPeople();
    Person? GetPerson(int id);
}

