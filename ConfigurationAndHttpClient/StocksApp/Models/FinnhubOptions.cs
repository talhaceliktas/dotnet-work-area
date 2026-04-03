namespace StocksApp.Models
{
    public class FinnhubOptions
    {
        public required string UserSecret { get; set; }
        public required string BaseUrl { get; set; }

        public required string DefaultStockSymbol { get; set; }
    }
}
