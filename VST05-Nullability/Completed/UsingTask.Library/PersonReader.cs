using System.Text.Json;
using UsingTask.Shared;

namespace UsingTask.Library;

public class PersonReader : IPersonReader
{
    private readonly HttpClient client = new() { BaseAddress = new Uri("http://localhost:9874") };
    private readonly JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public async Task<List<Person>> GetAsync(
        CancellationToken cancelToken = new CancellationToken())
    {
        await Task.Delay(3000, cancelToken);

        //throw new NotImplementedException("Jeremy did not implement GetAsync");

        cancelToken.ThrowIfCancellationRequested();

        HttpResponseMessage response =
            await client.GetAsync("people", cancelToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Unable to complete request: status code {response.StatusCode}");

        var stringResult =
            await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var result = JsonSerializer.Deserialize<List<Person>>(stringResult, options);

        //if (result is null)
        //    return new List<Person>();
        //return result;

        return result ?? new List<Person>();
    }
}
