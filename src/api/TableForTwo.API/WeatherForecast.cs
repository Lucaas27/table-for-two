namespace TableForTwo.API;

/// <summary>
/// A placeholder weather forecast response used by the scaffolded API.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// The forecast date.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// The forecast temperature in Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// The forecast temperature in Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// A short free-text weather summary.
    /// </summary>
    public string? Summary { get; set; }
}
