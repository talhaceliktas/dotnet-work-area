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
                StockSymbol = currentStockSymbol,
                CurrentPrice = Convert.ToDouble(responseDictionary?["c"].ToString()),
                Change = Convert.ToDouble(responseDictionary?["d"].ToString()),
                PercentChange = Convert.ToDouble(responseDictionary?["dp"].ToString()),
                HighPriceOfTheDay = Convert.ToDouble(responseDictionary?["h"].ToString()),
                LowPriceOfTheDay = Convert.ToDouble(responseDictionary?["l"].ToString()),
                OpenPriceOfTheDay = Convert.ToDouble(responseDictionary?["o"].ToString()),
                PreviousClosePrice = Convert.ToDouble(responseDictionary?["pc"].ToString()),
            };

            return View(stock);
        }
    }
}
