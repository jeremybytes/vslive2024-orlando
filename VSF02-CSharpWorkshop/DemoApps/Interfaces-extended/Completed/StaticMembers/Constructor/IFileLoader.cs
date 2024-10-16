namespace StaticMembers;

public interface IFileLoader
{
    // NOTE: This example is a work in progress,
    //       so don't take anything you see here
    //       too seriously.
    // static constructors on interfaces can be
    // used to initialize other static members.
    private static string fileName = "People.txt";

    public static string FileData;

    static IFileLoader()
    {
        string filePath = AppDomain.CurrentDomain.BaseDirectory + fileName;
        using (var reader = new StreamReader(filePath))
        {
            FileData = reader.ReadToEnd();
        }
    }
}
