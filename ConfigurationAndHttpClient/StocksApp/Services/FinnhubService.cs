using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.ServiceContracts;
using System.Text.Json;

namespace StocksApp.Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FinnhubOptions _finnhubOptions;

        public FinnhubService(IHttpClientFactory httpClientFactory, IOptions<FinnhubOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _finnhubOptions = options.Value;
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient()) {

                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{_finnhubOptions.BaseUrl}quote?symbol={stockSymbol}&token={_finnhubOptions.UserSecret}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

                using var stream = await responseMessage.Content.ReadAsStreamAsync();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();

                Dictionary<string, object>? dictionary =
                    JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                if (dictionary == null) {
                    throw new InvalidOperationException("No response from finnhub server");
                }
                if (dictionary.ContainsKey("error")) {
                    throw new InvalidOperationException("Error");
                }

                return dictionary;
            }
        }
    }
}
