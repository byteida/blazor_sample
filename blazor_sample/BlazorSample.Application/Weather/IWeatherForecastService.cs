using BlazorSample.Domain.Weather;

namespace BlazorSample.Application.Weather;

/// <summary>
/// Defines the application use case for retrieving weather forecasts.
/// </summary>
public interface IWeatherForecastService
{
    Task<IReadOnlyList<WeatherForecast>> GetForecastAsync(
        DateOnly startDate,
        int days,
        CancellationToken cancellationToken = default);
}
