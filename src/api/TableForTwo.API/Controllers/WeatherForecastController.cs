using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TableForTwo.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpPost("validate")]
    public IActionResult Validate([FromBody] ForecastRequest request)
    {
        return Accepted(new { request.Name, request.Servings });
    }

    [HttpGet("missing")]
    public IActionResult Missing()
    {
        return NotFound();
    }

    [HttpPost("conflict")]
    public IActionResult ConflictExample()
    {
        return Conflict();
    }

    [HttpGet("fail")]
    public IActionResult Fail()
    {
        throw new InvalidOperationException("Simulated failure for error contract verification.");
    }

    public sealed class ForecastRequest
    {
        [Required]
        public string? Name { get; init; }

        [Range(1, 20)]
        public int Servings { get; init; }
    }
}
