using Entities;

namespace ServiceContracts
{
    public interface IWeatherService
    {
        Task<WeatherReport> GetCurrentWeatherAsync(string city);
    }
}
