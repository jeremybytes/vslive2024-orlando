using Microsoft.AspNetCore.Mvc;
using PeopleViewer.Common;
using PersonDataReader.Service;

namespace PeopleViewer.Controllers;

public class PeopleController : Controller
{
    ServiceReader reader = new ServiceReader();

    public async Task<IActionResult> UseInjectedReader()
    {
        ViewData["Title"] = "Using Injected Reader";
        //ViewData["ReaderType"] = reader.GetTypeName();

        List<Person> people = (await reader.GetPeople()).ToList();
        return View("Index", people);
    }
}
