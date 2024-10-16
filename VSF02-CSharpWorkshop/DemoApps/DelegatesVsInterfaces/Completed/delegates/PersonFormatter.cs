namespace delegates;

public interface IPersonFormatter
{
    string Format(Person person);
}

public class DefaultFormatter : IPersonFormatter
{
    public string Format(Person person)
    {
        return person.ToString();
    }
}

public class FamilyNameFormatter : IPersonFormatter
{
    public string Format(Person person)
    {
        return person.FamilyName.ToUpper();
    }
}

public class GivenNameFormatter : IPersonFormatter
{
    public string Format(Person person)
    {
        return person.GivenName.ToLower();
    }
}

public class FullNameFormatter : IPersonFormatter
{
    public string Format(Person person)
    {
        return $"{person.FamilyName}, {person.GivenName}";
    }
}
