namespace PersonDataReader.CSV.Test;

public class FakeFileLoader : ICSVFileLoader
{
    private string _dataType;

    public FakeFileLoader(string dataType)
    {
        _dataType = dataType;
    }

    public async Task<IReadOnlyCollection<string>> LoadFile()
    {
        await Task.Delay(1);
        return _dataType switch
        {
            "Good" => TestData.WithGoodRecords,
            "Mixed" => TestData.WithGoodAndBadRecords,
            "Bad" => TestData.WithOnlyBadRecords,
            "Empty" => new List<string>(),
            _ => TestData.WithGoodAndBadRecords,
        };
    }
}
