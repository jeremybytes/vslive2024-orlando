namespace AccessModifiers.Public;

public class Customer
{
    // Class members default to "private"
    // These fields are both "private"
    string? _familyName;
    private string? _givenName;

    public int Id { get; set; }

    public string FamilyName
    {
        get => _familyName ?? "";
        set => _familyName = value;
    }

    public string GivenName
    {
        get => _givenName ?? "";
        set => _givenName = value;
    }
}
