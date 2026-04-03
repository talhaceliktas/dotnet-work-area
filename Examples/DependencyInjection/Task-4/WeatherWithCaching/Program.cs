using Microsoft.Extensions.Caching.Memory;
using ServiceContracts;
using Services;
using Services.Decorators;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
builder.Services.AddScoped<RealWeatherService>();
builder.Services.AddScoped<IWeatherService>(sp =>
    new CachingWeatherServiceDecorator(
        sp.GetRequiredService<RealWeatherService>(),
        sp.GetRequiredService<IMemoryCache>()
    ));

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();
