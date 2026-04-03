using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Services;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnhubService _finnhubService;
        private readonly FinnhubOptions _finnhubOptions;

        public HomeController(FinnhubService finnhubService, IOptions<FinnhubOptions> options) {
            _finnhubService = finnhubService;
            _finnhubOptions = options.Value;
        }

        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            Dictionary<string, object>? responseDictionary = 
            await _finnhubService.GetStockPriceQuote(_finnhubOptions.DefaultStockSymbol);

            return View();
        }
    }
}
