var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(9874));

// Add services to the container.
builder.Services.AddSingleton<IPeopleProvider, HardCodedPeopleProvider>();

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.WriteIndented = true);

var app = builder.Build();

app.MapGet("/people", (IPeopleProvider provider) =>
    {
        return provider.GetPeople();
    })
    .WithName("GetPeople");

app.MapGet("/people/{id}", (IPeopleProvider provider, int id) =>
    {
        return provider.GetPeople().FirstOrDefault(p => p.Id == id);
    })
    .WithName("GetPersonById");

app.MapGet("/people/ids", 
    (IPeopleProvider provider) => provider.GetPeople().Select(p => p.Id).ToList())
    .WithName("GetAllPersonIds");

app.Run();
