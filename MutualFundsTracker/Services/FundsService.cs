using MutualFundsTracker.Helpers;
using MutualFundsTracker.Models;
using System;
using System.Collections.Generic;
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
            string totalValue;

            try
            {
                var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/home/TotalValue"));
                if (response.IsSuccessStatusCode)
                {
                    totalValue = await response.Content.ReadAsStringAsync();
                }
                else 
                {
                    throw new CustomException(response.Content.ReadAsStringAsync().Result);
                }

            }
            catch(CustomException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }

            return totalValue;
        }

        public async Task<ValueTrend[]> GetTotalValueTrendAsync()
        {
            try
            {
                var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/home/ValueTrend/12"));
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
            catch (CustomException)
            {
                throw;
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
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

    }
}
