namespace HouseControl.Sunset;

public interface ISolarService
{
    Task<string> GetServiceData(DateOnly date);
}

public class SolarService : ISolarService
{
    private static readonly HttpClient client =
        new() { BaseAddress = new Uri("http://localhost:8973") };

    public async Task<string> GetServiceData(DateOnly date)
    {
        HttpResponseMessage response =
            await client.GetAsync($"SolarCalculator/51.520/-0.0963/{date:yyyy-MM-dd}").ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Unable to complete request: status code {response.StatusCode}");

        var stringResult =
            await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return stringResult;
    }
}
