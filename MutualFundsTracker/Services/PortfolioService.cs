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
    public class PortfolioService
    {
        private readonly string apiURL;
        static readonly HttpClient httpClient = new HttpClient();

        public PortfolioService(string URL)
        {
            apiURL = URL;
        }

        public async Task<FundDistribution[]> GetFundDistributionAsync()
        {
            try
            {
                var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/portfolio/funddistribution"));
                var content = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true
                };
                var fundsValueArray = JsonSerializer.Deserialize<FundDistribution[]>(content, options);
                var result = Utils.GetFundDistributionPercentage(fundsValueArray);
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

        public async Task<List<PortfolioSnapshot>> GetPortfolioSnapshotAsync()
        {
            try
            {
                var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/portfolio/portfoliosnapshot"));
                var content = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true
                };
                var result = JsonSerializer.Deserialize<List<PortfolioSnapshot>>(content, options);
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

        public async Task<InvestmentStatus> GetInvestmentReturnsValueAsync()
        {
            try
            {
                var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/portfolio/investmentvalue"));
                var content = await response.Content.ReadAsStringAsync();
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true
                };
                var result = JsonSerializer.Deserialize<InvestmentStatus>(content,options);
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

            return default;
        }
    }
}
