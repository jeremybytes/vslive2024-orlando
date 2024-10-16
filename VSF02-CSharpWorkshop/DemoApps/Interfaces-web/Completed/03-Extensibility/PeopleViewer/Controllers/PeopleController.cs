using Microsoft.AspNetCore.Mvc;
using PersonReader.Interface;

namespace PeopleViewer.Controllers;

public class PeopleController : Controller
{
    public async Task<IActionResult> UseService()
    {
        ViewData["Title"] = "From Web Service";
        return await FetchData("Service");
    }

    public async Task<IActionResult> UseCSV()
    {
        ViewData["Title"] = "From CSV Text File";
        return await FetchData("CSV");
    }

    public async Task<IActionResult> UseSQL()
    {
        ViewData["Title"] = "From SQL Database";
        return await FetchData("SQL");
    }

    private async Task<IActionResult> FetchData(string readerType)
    {
        IPersonReader reader = ReaderFactory.GetReader(readerType);

        ViewData["ReaderType"] = reader.GetType().ToString();

        var people = await reader.GetPeople();
        return View("Index", people);
    }
}
