using Microsoft.AspNetCore.Mvc;
using StocksApp.Services;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnhubService _myService;
        public HomeController(FinnhubService myService) {
            _myService = myService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            await _myService.method();

            return View();
        }
    }
}
