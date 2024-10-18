using Microsoft.AspNetCore.Mvc;
using SolarCalculator.Service.Models;

namespace SolarCalculator.Service.Controllers;

[Route("[controller]")]
[ApiController]
public class SolarCalculatorController : ControllerBase
{
    [HttpGet("")]
    public SolarData GetWithQueryString(double lat, double lng, DateTime date)
    {
        var result = SolarCalculatorProvider.GetSolarTimes(date, lat, lng);
        return result;
    }

    [HttpGet("{lat}/{lng}/{date}")]
    public SolarData GetWithRoute(double lat, double lng, DateTime date)
    {
        var result = SolarCalculatorProvider.GetSolarTimes(date, lat, lng);
        return result;
    }
}
