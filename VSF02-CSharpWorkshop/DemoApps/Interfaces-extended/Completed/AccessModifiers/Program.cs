using AccessModifiers.Private;
using AccessModifiers.Protected;
using AccessModifiers.Public;

class Program
{
    static void Main(string[] args)
    {
        // Private Members
        IDistanceCalculator calculator = new SquareDistanceCalculator();
        int distance = calculator.CalculateMetadistance((1, 7), (3, 1));
        Console.WriteLine($"Metadistance = {distance}");

        // Protected Members
        IInventoryController controller = new FakeInventoryController();
        var item = new InventoryItem(7);
        controller.PushInventoryItem(item);
        // Protected members cannot be accessed
        // through the implementation.
        // Protected members may only be useful in interface
        // hierarchies since they cannot be access through
        // implementers
        //controller.PullInventoryItem(7);

        // Public Members
        // Public members are accessible through implementers
        ICustomerReader reader = new FakeCustomerReader();
        var customerList = reader.GetCustomers();
        DisplayCustomerList(customerList);

        Console.ReadLine();
    }


    static void DisplayCustomerList(System.Collections.Generic.IReadOnlyCollection<Customer> customerList)
    {
        Console.WriteLine("=======================");
        int count = 1;
        foreach (var customer in customerList)
        {
            Console.WriteLine($"Item #{count}");
            Console.WriteLine($"Customer Id: {customer.Id}");
            Console.WriteLine($"Given Name: {customer.GivenName}");
            Console.WriteLine($"Family Name: {customer.FamilyName}");
            Console.WriteLine("---");
            count++;
        }
        Console.WriteLine("=======================");
    }
}