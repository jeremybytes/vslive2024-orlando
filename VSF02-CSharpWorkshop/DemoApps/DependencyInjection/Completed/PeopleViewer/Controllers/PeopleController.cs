using Microsoft.AspNetCore.Mvc;
using PeopleViewer.Common;

namespace PeopleViewer.Controllers;

public class PeopleController : Controller
{
    private IPersonReader reader;

    public PeopleController(IPersonReader personReader)
    {
        reader = personReader;
    }

    public async Task<IActionResult> UseConstructorReader()
    {
        ViewData["Title"] = "Using Constructor Reader";
        ViewData["ReaderType"] = reader.GetTypeName();

        List<Person> people = (await reader.GetPeople()).ToList();
        return View("Index", people);
    }

    public async Task<IActionResult> UseMethodReader([FromKeyedServices("method")] IPersonReader methodReader)
    {
        ViewData["Title"] = "Using Method Reader";
        ViewData["ReaderType"] = methodReader.GetTypeName();

        List<Person> people = (await methodReader.GetPeople()).ToList();
        return View("Index", people);
    }
}
