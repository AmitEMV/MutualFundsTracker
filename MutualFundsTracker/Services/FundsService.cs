using MutualFundsTracker.Helpers;
using MutualFundsTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MutualFundsTracker.Services
{
    public class FundsService
    {
        private readonly string apiURL;
        static readonly HttpClient httpClient = new HttpClient();

        public FundsService(string URL)
        {
            apiURL = URL;
        }

        public async Task<string> GetTotalValueAsync()
        {
            var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/home/GetTotalValue"));
            string totalValue = await response.Content.ReadAsStringAsync();
            return totalValue;
        }

        public async Task<ValueTrend[]> GetTotalValueTrendAsync()
        {
            try
            {
                UriBuilder uriBuilder = new UriBuilder($"{apiURL}/api/home/GetValueTrend")
                {
                    Query = "numOfMonths=12"
                };
                var response = await httpClient.GetAsync(uriBuilder.Uri);
                var content = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new DateTimeParser());
                var result = JsonSerializer.Deserialize<ValueTrend[]>(content, options);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<List<FundPerformance>> GetFundPerformanceAsync(string API)
        {
            try
            {
                var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/home/{API}"));
                var content = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true
                };
                var result = JsonSerializer.Deserialize<List<FundPerformance>>(content, options);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

    }
}
