using PeopleViewer.Common;
using System.Text.Json;

namespace PeopleViewer.Library;

public class PersonDataReader
{
    HttpClient client = new();
    JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };

    public PersonDataReader()
    {
        client.BaseAddress = new Uri("http://localhost:9874");
    }

    public async Task<List<Person>> GetPeople()
    {
        HttpResponseMessage response = await client.GetAsync("people");
        if (!response.IsSuccessStatusCode)
        {
            return new();
        }

        var stringResult = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Person>>(stringResult, options) ?? new();
    }
}