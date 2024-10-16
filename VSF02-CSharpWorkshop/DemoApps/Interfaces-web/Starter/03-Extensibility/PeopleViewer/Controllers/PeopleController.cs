using Microsoft.AspNetCore.Mvc;

namespace PeopleViewer.Controllers;

public class PeopleController : Controller
{
    public async Task<IActionResult> UseService()
    {
        ViewData["Title"] = "From Web Service";
        ViewData["ReaderType"] = "";

        await Task.Delay(1);

        return View("Index", null);
    }

    public async Task<IActionResult> UseCSV()
    {
        ViewData["Title"] = "From CSV Text File";
        ViewData["ReaderType"] = "";

        await Task.Delay(1);

        return View("Index", null);
    }

    public async Task<IActionResult> UseSQL()
    {
        ViewData["Title"] = "From SQL Database";
        ViewData["ReaderType"] = "";

        await Task.Delay(1);

        return View("Index", null);
    }
}
