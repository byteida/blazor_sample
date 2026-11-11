namespace BlazorSample.Domain.Weather;

public sealed record WeatherForecast(
    DateOnly Date,
    int TemperatureC,
    string Summary)
{
    public int TemperatureF => 32 + (int)Math.Round(TemperatureC * 9d / 5d);
}
