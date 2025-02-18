using System;
using System.Text.Json;
using Common;
using StackExchange.Redis;

namespace BackEnd;

public class WeatherService(
    HttpClient httpClient,
    IConnectionMultiplexer redis,
    ILogger<WeatherService> logger)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IConnectionMultiplexer redis = redis;
    private readonly ILogger<WeatherService> logger = logger;

    public async Task<WeatherResponse?> GetWeatherAsync(string city)
    {
        logger.LogInformation("Getting weather for {City} from web api", city); // not actually getting weather for the city but just random weather data 
        
        var cache = redis.GetDatabase();
        var cacheKey = city;
        var forecast = Array.Empty<WeatherForecast>();
        if (cache.KeyExists(cacheKey))
        {
            var weatherData = cache.StringGet(cacheKey);
            if (!weatherData.HasValue)
                return null;

            logger.LogInformation("Weather data retrieved from cache for {City}", city);
            forecast =  JsonSerializer.Deserialize<WeatherForecast[]>(weatherData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return new WeatherResponse
            {
                WeatherForecasts = forecast,
                Message = "Weather data retrieved successfully from cache"
            };
        }
        else 
        {
            var response = await _httpClient.GetAsync("/weather");
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            forecast =  JsonSerializer.Deserialize<WeatherForecast[]>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            cache.StringSet(cacheKey, content, TimeSpan.FromMinutes(1));
            return new WeatherResponse { 
                WeatherForecasts = forecast,
                Message = "Weather data retrieved successfully from weather api"
            };
        }
    }
}