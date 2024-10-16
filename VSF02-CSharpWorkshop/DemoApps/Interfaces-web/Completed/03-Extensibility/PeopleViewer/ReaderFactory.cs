using PersonReader.CSV;
using PersonReader.Interface;
using PersonReader.Service;
using PersonReader.SQL;

namespace PeopleViewer;

public static class ReaderFactory
{
    public static IPersonReader GetReader(string readerType)
    {
        return readerType switch
        {
            "Service" => new ServiceReader(),
            "CSV" => new CSVReader(),
            "SQL" => new SQLReader(),
            _ => throw new ArgumentException("Invalid reader type"),
        };
    }
}
