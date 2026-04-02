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
            ViewBag.MyKey = _configuration.GetValue<string>("MyKey", "Test");
            ViewBag.MyAPIKey = _configuration.GetValue<string>("MyApiKey", "AAADDSSSS22222");

            return View();
        }
    }
}
