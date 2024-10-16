using PeopleViewer.Common;
using System.Text.Json;

namespace PersonDataReader.Service;

public class ServiceReader
{
    HttpClient client = new HttpClient();
    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public ServiceReader()
    {
        client.BaseAddress = new Uri("http://localhost:9874");
    }

    public async Task<IReadOnlyCollection<Person>> GetPeople()
    {
        HttpResponseMessage response = await client.GetAsync("people");
        if (!response.IsSuccessStatusCode)
        {
            return new List<Person>();
        }

        var stringResult = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Person>>(stringResult, options) ?? new();
    }

    public async Task<Person?> GetPerson(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"people/{id}");
        if (!response.IsSuccessStatusCode)
        {
            return new Person();
        }

        var stringResult = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Person>(stringResult, options) ?? new();
    }
}