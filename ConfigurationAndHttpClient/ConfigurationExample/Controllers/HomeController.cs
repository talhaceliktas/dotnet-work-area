using Microsoft.AspNetCore.Mvc;

namespace ConfigurationExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration) {
            _configuration = configuration;
        }


        [HttpGet("/")]
        public IActionResult Index()
        {
            //ViewBag.ClientID = _configuration.GetValue<string>("WeatherAPI:ClientID", "Default ID");
            //ViewBag.ClientSecret = _configuration.GetValue<string>("WeatherAPI:ClientSecret", "Default Secret");

            //ViewBag.ClientID = _configuration.GetSection("WeatherAPI").GetValue<string>("ClientID", "Default ID");
            //ViewBag.ClientSecret = _configuration.GetSection("WeatherAPI").GetValue<string>("ClientSecret", "Default Secret");

            //IConfigurationSection weatherApiSection = _configuration.GetSection("WeatherAPI");

            //ViewBag.ClientID = weatherApiSection["ClientID"];
            //ViewBag.ClientSecret = weatherApiSection["ClientSecret"];

            WeatherApiOptions? weatherApiSection = _configuration.GetSection("WeatherAPI")
                .Get<WeatherApiOptions>();


            ViewBag.ClientID = weatherApiSection?.ClientID ?? "Default Client ID";
            ViewBag.ClientSecret = weatherApiSection?.ClientSecret ?? "Default Secret";

            return View();
        }
    }
}
