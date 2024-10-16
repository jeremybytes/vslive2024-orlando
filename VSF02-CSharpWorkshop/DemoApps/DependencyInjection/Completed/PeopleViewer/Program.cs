using PeopleViewer.Common;
using PersonDataReader.CSV;
using PersonDataReader.Decorators;
using PersonDataReader.SQL;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(5000));

// Add services to the container.
//builder.Services.AddSingleton<IPersonReader, CSVReader>();
builder.Services.AddSingleton<IPersonReader>(s => GetReader());
builder.Services.AddKeyedSingleton<IPersonReader, SQLReader>("method");

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static IPersonReader GetReader()
{
    return new CachingReader(new CSVReader());
}