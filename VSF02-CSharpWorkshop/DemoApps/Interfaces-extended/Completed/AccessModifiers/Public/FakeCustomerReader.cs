namespace AccessModifiers.Public;

public class FakeCustomerReader : ICustomerReader
{
    public Customer GetCustomer(int id)
    {
        Customer customer = new() { Id = id, GivenName = "Abigail" };
        
        // _familyName is not accessible because it is "private"
        // customer._familyName = "Artois";
        
        customer.FamilyName = "Artois";
        return customer;
    }

    public IReadOnlyCollection<Customer> GetCustomers()
    {
        var list = new List<Customer>();
        list.Add(new() { Id = 2, GivenName = "Marcel", FamilyName = "Dallas" });
        list.Add(new() { Id = 4, GivenName = "Ansel", FamilyName = "Meridius" });
        return list;
    }
}
