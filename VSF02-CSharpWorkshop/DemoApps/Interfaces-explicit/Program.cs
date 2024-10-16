namespace explicit_implementation;

class Program
{
    static void Main(string[] args)
    {
        Catalog catalog = new Catalog();
        string result = catalog.Save();
        // result = "Catalog Save"
        Console.WriteLine(result);

        ISaveable saveable = new Catalog();
        result = saveable.Save();
        // result = "Interface Save"
        Console.WriteLine(result);

        saveable = (ISaveable)catalog;
        result = saveable.Save();
        // result = "Interface Save"
        Console.WriteLine(result);
    }
}
