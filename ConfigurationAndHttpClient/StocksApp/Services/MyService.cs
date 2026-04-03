using Microsoft.Extensions.Options;
using StocksApp.Models;

namespace StocksApp.Services
{
    public class MyService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FinnhubOptions _finnhubOptions;

        public MyService(IHttpClientFactory httpClientFactory, IOptions<FinnhubOptions> options)
        {
            _httpClientFactory = httpClientFactory;
            _finnhubOptions = options.Value;
        }

        public async Task method()
        {
            using (HttpClient httpClient = _httpClientFactory.CreateClient()) {

                HttpRequestMessage requestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri($"{_finnhubOptions.BaseUrl}quote?symbol=AAPL&token={_finnhubOptions.UserSecret}"),
                    Method = HttpMethod.Get,
                };

                HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

                using var stream = await responseMessage.Content.ReadAsStreamAsync();

                StreamReader streamReader = new StreamReader(stream);

                string response = streamReader.ReadToEnd();
            }
        }
    }
}
