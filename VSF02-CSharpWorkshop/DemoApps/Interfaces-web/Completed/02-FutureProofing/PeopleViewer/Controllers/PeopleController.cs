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

        List<Person> people = await reader.GetPeople();
        return View("Index", people);
    }

    public async Task<IActionResult> AbstractPeople()
    {
        ViewData["Title"] = "Abstract People";
        ViewData["ReaderType"] = reader.GetType().ToString();

        IEnumerable<Person> people = await reader.GetPeople();
        return View("Index", people);
    }
}
