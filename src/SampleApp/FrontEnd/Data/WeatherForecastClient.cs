using Common;

namespace FrontEnd.Data;

public class WeatherForecastClient
{
    private HttpClient _httpClient;
    private ILogger<WeatherForecastClient> _logger;

    public WeatherForecastClient(HttpClient httpClient, ILogger<WeatherForecastClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<WeatherResponse?> GetForecastAsync(string? city)
        => await _httpClient.GetFromJsonAsync<WeatherResponse>($"weather/{city}");
}