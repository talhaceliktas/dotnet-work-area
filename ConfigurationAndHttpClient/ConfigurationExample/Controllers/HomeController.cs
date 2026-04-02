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
            ViewBag.ClientID = _configuration.GetValue<string>("WeatherAPI:ClientID", "Default ID");
            ViewBag.ClientSecret = _configuration.GetValue<string>("WeatherAPI:ClientSecret", "Default Secret");

            return View();
        }
    }
}
