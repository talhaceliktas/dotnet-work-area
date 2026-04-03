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

        [HttpGet("/{stockSymbol?}")]
        public async Task<IActionResult> Index(string? stockSymbol)
        {
            string currentStockSymbol = stockSymbol ?? _finnhubOptions.DefaultStockSymbol;

            Dictionary<string, object>? responseDictionary = 
            await _finnhubService.GetStockPriceQuote(currentStockSymbol);

            Stock stock = new Stock() {
                StockSymbol = stockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionary?["c"] ?? 0),
                Change = Convert.ToDouble(responseDictionary?["d"] ?? 0),
                PercentChange = Convert.ToDouble(responseDictionary?["dp"] ?? 0),
                HighPriceOfTheDay = Convert.ToDouble(responseDictionary?["h"] ?? 0),
                LowPriceOfTheDay = Convert.ToDouble(responseDictionary?["l"] ?? 0),
                OpenPriceOfTheDay = Convert.ToDouble(responseDictionary?["o"] ?? 0),
                PreviousClosePrice = Convert.ToDouble(responseDictionary?["pc"] ?? 0),
            };

            return View();
        }
    }
}
