using System;
using System.Net.Http;
using System.Text.Json;

namespace All.About.Objects
{
    public class ExchangeRates
    {
        private readonly HttpClient _httpClient;

        public ExchangeRates()
        {
            _httpClient = new HttpClient();
        }

        public ExchangeResult Latest()
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://api.exchangeratesapi.io/latest?base=GBP"),
                Method = HttpMethod.Get
            };

            var response = _httpClient.SendAsync(request).Result;
            var content = response.Content.ReadAsStringAsync().Result;

            var result = JsonSerializer.Deserialize<ExchangeResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result;
        }
    }
}