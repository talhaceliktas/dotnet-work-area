using Entities;
using ServiceContracts;

namespace Services
{
    public class RealWeatherService : IWeatherService
    {
        public async Task<WeatherReport> GetCurrentWeatherAsync(string city)
        {
            await Task.Delay(2000);

            Random random = new Random();
            double randomNumber = random.NextDouble() * 35;

            WeatherReport weatherReport = new WeatherReport
            {
                City = city,
                TemperatureCelsius = Math.Round(randomNumber, 1),
                Description = "Parçalı Bulutlu", 
                FetchedAt = DateTime.Now 
            };

            return weatherReport;
            
        }
    }
}
