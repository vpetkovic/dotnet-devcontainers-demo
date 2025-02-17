using BackEnd;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<WeatherService>(client =>
{
    var weatherApi = builder.Configuration["WeatherApi:Url"] ?? throw new ArgumentNullException("WeatherApi:Url is not configured");
    client.BaseAddress = new Uri(weatherApi);
});

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(builder.Configuration["Redis"])); 

//builder.Services.AddTransient<WeatherService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/weather/{city}", async (
    string city, 
    WeatherService weatherService) =>
{
    var result = await weatherService.GetWeatherAsync(city);
    return result is not null ? Results.Ok(result) : Results.NotFound("Weather data not available.");
});

app.Run();