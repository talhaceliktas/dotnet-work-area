using Entities;
using Microsoft.Extensions.Caching.Memory;
using ServiceContracts;
namespace Services.Decorators
{
    public class CachingWeatherServiceDecorator : IWeatherService
    {
        private readonly IWeatherService _inner;
        private readonly IMemoryCache _cache;

        public CachingWeatherServiceDecorator(IWeatherService inner, IMemoryCache cache) {
            _inner = inner;
            _cache = cache;
        }

        public async Task<WeatherReport> GetCurrentWeatherAsync(string city)
        {
            string cacheKey = $"weather_{city.ToLower()}";

            var weatherReport = await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

                Console.WriteLine($"[CACHE MISS] {city} için veri önbellekte yok, API'ye gidiliyor...");

                return await _inner.GetCurrentWeatherAsync(city);
            });

            return weatherReport ?? throw new InvalidOperationException("Hava durumu verisi çekilemedi!");
        }
    }
}
