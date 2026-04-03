using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly WeatherApiOptions _weatherOptions;

        public HomeController(IConfiguration configuration, IOptions<WeatherApiOptions> options) {
            _configuration = configuration;

            _weatherOptions = options.Value;
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


            //WeatherApiOptions? weatherApiSection = _configuration.GetSection("WeatherAPI")
            //    .Get<WeatherApiOptions>();


            //ViewBag.ClientID = weatherApiSection?.ClientID ?? "Default Client ID";
            //ViewBag.ClientSecret = weatherApiSection?.ClientSecret ?? "Default Secret";

            ViewBag.ClientID = _weatherOptions.ClientID;
            ViewBag.ClientSecret = _weatherOptions.ClientSecret;

            return View();
        }
    }
}
