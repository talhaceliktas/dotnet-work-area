using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace WeatherWithCaching.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWeatherService _weatherService; 

        public HomeController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("weather/{city}")]
        public async Task<IActionResult> Index([FromRoute] string city)
        {
            var weatherData = await _weatherService.GetCurrentWeatherAsync(city);

            return Ok(weatherData);
        }
    }
}
