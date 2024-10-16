using Microsoft.AspNetCore.Mvc;
using PersonReader.Interface;

namespace PeopleViewer.Controllers;

public class PeopleController : Controller
{
    private ReaderFactory readerFactory;

    public PeopleController(IConfiguration configuration)
    {
        readerFactory = new ReaderFactory(configuration);
    }

    public async Task<IActionResult> UseDynamicReader()
    {
        IPersonReader reader = readerFactory.GetReader();

        ViewData["Title"] = "From Dynamic Reader";
        ViewData["ReaderType"] = reader.GetType().ToString();

        var people = await reader.GetPeople();
        return View("Index", people);
    }
}
