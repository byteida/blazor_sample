using BlazorSample.Domain.Weather;
using BlazorSample.Infrastructure.Weather;
using Xunit;

namespace BlazorSample.Tests.Weather;

public sealed class DemoWeatherForecastServiceTests
{
    private readonly DemoWeatherForecastService _service = new();

    [Fact]
    public async Task GetForecastAsync_ReturnsRequestedNumberOfForecasts()
    {
        var startDate = new DateOnly(2026, 1, 1);

        var forecasts = await _service.GetForecastAsync(startDate, 5);

        Assert.Equal(5, forecasts.Count);
        Assert.Equal(startDate.AddDays(1), forecasts[0].Date);
        Assert.Equal(startDate.AddDays(5), forecasts[4].Date);
    }

    [Fact]
    public async Task GetForecastAsync_ReturnsDeterministicResultsForSameDate()
    {
        var startDate = new DateOnly(2026, 5, 1);

        var first = await _service.GetForecastAsync(startDate, 3);
        var second = await _service.GetForecastAsync(startDate, 3);

        Assert.Equal(first, second);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(15)]
    public async Task GetForecastAsync_RejectsInvalidDayCount(int days)
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
            () => _service.GetForecastAsync(DateOnly.FromDateTime(DateTime.Today), days));
    }

    [Fact]
    public async Task GetForecastAsync_ThrowsWhenCancellationWasRequested()
    {
        using var cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        await Assert.ThrowsAnyAsync<OperationCanceledException>(
            () => _service.GetForecastAsync(
                DateOnly.FromDateTime(DateTime.Today),
                5,
                cancellation.Token));
    }

    [Fact]
    public void WeatherForecast_CalculatesFahrenheitFromCelsius()
    {
        var forecast = new WeatherForecast(
            new DateOnly(2026, 1, 1),
            TemperatureC: 0,
            Summary: "Cold");

        Assert.Equal(32, forecast.TemperatureF);
    }
}
