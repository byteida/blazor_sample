using BlazorSample.Application.Weather;
using BlazorSample.Domain.Weather;

namespace BlazorSample.Infrastructure.Weather;

/// <summary>
/// Supplies deterministic sample data. Replace this adapter with a real provider
/// without changing the application contract or the UI.
/// </summary>
public sealed class DemoWeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild",
        "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public Task<IReadOnlyList<WeatherForecast>> GetForecastAsync(
        DateOnly startDate,
        int days,
        CancellationToken cancellationToken = default)
    {
        if (days is < 1 or > 14)
        {
            throw new ArgumentOutOfRangeException(
                nameof(days),
                days,
                "The number of forecast days must be between 1 and 14.");
        }

        cancellationToken.ThrowIfCancellationRequested();

        IReadOnlyList<WeatherForecast> forecasts = Enumerable
            .Range(1, days)
            .Select(offset =>
            {
                var date = startDate.AddDays(offset);
                var seed = date.DayNumber;

                return new WeatherForecast(
                    date,
                    TemperatureC: (seed * 17 % 76) - 20,
                    Summary: Summaries[seed % Summaries.Length]);
            })
            .ToArray();

        return Task.FromResult(forecasts);
    }
}
