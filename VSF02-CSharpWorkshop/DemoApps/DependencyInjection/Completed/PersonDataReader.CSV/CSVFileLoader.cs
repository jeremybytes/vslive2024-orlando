namespace PersonDataReader.CSV;

public interface ICSVFileLoader
{
    Task<IReadOnlyCollection<string>> LoadFile();
}

public class CSVFileLoader : ICSVFileLoader
{
    private string filePath;

    public CSVFileLoader(string filePath)
    {
        this.filePath = filePath;
    }

    public async Task<IReadOnlyCollection<string>> LoadFile()
    {
        var data = new List<string>();

        using var reader = new StreamReader(filePath);
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            data.Add(line);
        }

        return data;
    }
}
