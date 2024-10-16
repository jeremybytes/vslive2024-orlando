using Microsoft.AspNetCore.Mvc;
using PeopleViewer.Common;
using PeopleViewer.Library;

namespace PeopleViewer.Controllers;

public class PeopleController : Controller
{
    PersonDataReader reader = new();

    public async Task<IActionResult> ConcretePeople()
    {
        ViewData["Title"] = "Concrete People";
        ViewData["ReaderType"] = reader.GetType().ToString();

        await Task.Delay(1);
        return View("Index", null);
    }

    public async Task<IActionResult> AbstractPeople()
    {
        ViewData["Title"] = "Abstract People";
        ViewData["ReaderType"] = reader.GetType().ToString();

        await Task.Delay(1);
        return View("Index", null);
    }
}
