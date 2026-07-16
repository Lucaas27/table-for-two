using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TableForTwo.API.Controllers;

[ApiController]
[Route("[controller]")]
/// <summary>
/// Exposes scaffolded endpoints used to verify API hosting and error behaviour.
/// </summary>
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    /// <summary>
    /// Returns a small set of sample forecasts.
    /// </summary>
    /// <returns>The generated weather forecasts.</returns>
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

    /// <summary>
    /// Validates the sample request payload.
    /// </summary>
    /// <param name="request">The sample request payload.</param>
    /// <returns>An accepted response when the payload is valid.</returns>
    [HttpPost("validate")]
    public IActionResult Validate([FromBody] ForecastRequest request)
    {
        return Accepted(new { request.Name, request.Servings });
    }

    /// <summary>
    /// Returns a sample not-found response.
    /// </summary>
    /// <returns>A not-found result.</returns>
    [HttpGet("missing")]
    public IActionResult Missing()
    {
        return NotFound();
    }

    /// <summary>
    /// Returns a sample conflict response.
    /// </summary>
    /// <returns>A conflict result.</returns>
    [HttpPost("conflict")]
    public IActionResult ConflictExample()
    {
        return Conflict();
    }

    /// <summary>
    /// Throws a sample exception to exercise server-error handling.
    /// </summary>
    /// <returns>This method does not return successfully.</returns>
    [HttpGet("fail")]
    public IActionResult Fail()
    {
        throw new InvalidOperationException("Simulated failure for error contract verification.");
    }

    /// <summary>
    /// A sample request payload used to exercise validation behaviour.
    /// </summary>
    public sealed class ForecastRequest
    {
        /// <summary>
        /// The sample forecast name.
        /// </summary>
        [Required]
        public string? Name { get; init; }

        /// <summary>
        /// The number of servings in the sample request.
        /// </summary>
        [Range(1, 20)]
        public int Servings { get; init; }
    }
}
