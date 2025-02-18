using Common;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace BackEnd.Tests;

public class WeatherTests
{
    [Fact]
    public async Task ShouldReturnWeatherData()
    {
        // Arrange
        var weaterService = new WeatherServiceStub();
        var city = "Seattle";

        // Act
        var result = await weaterService.GetWeatherAsync(city);

        // Assert
        Assert.NotNull(result);
    }

    class WeatherServiceStub : IWeatherService
    {
        public WeatherServiceStub()
        {
        }

        public async Task<WeatherResponse?> GetWeatherAsync(string city)
        {
            var weather =  new WeatherResponse
            {
                WeatherForecasts = new WeatherForecast[]
                {
                    new WeatherForecast
                    {
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        TemperatureC = 20,
                        Summary = "Sunny"
                    }
                },
                Message = "Weather data retrieved successfully from cache"
            };

            return await Task.FromResult(weather);
        }
    }
}