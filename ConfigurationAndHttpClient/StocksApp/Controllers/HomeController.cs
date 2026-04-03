using Microsoft.AspNetCore.Mvc;
using StocksApp.Services;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnhubService _finnhubService;
        public HomeController(FinnhubService finnhubService) {
            _finnhubService = finnhubService;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, object>? responseDictionary = 
            await _finnhubService.GetStockPriceQuote("MSFT");

            return View();
        }
    }
}
