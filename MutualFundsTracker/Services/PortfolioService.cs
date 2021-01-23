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

        public async Task<bool> RemoveFundFromFolio(PortfolioSnapshot portfolioSnapshot)
        {
            var result = false;

            try
            {
                var requestMessage = new HttpRequestMessage
                {
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri($"{apiURL}/api/portfolio/deletefund"),
                    Content = new StringContent(JsonSerializer.Serialize(portfolioSnapshot), System.Text.Encoding.UTF8, "application/json")
                };

                var response = await httpClient.SendAsync(requestMessage);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        IgnoreNullValues = true,
                        PropertyNameCaseInsensitive = true
                    };
                    result = JsonSerializer.Deserialize<bool>(content, options);
                }
                else
                {
                    throw new CustomException(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public async Task<bool> AddFundToFolio(FundInfo fundInfo)
        {
            var result = false;

            try
            {
                var response = await httpClient.PostAsync(new Uri($"{apiURL}/api/portfolio/addfund"), new StringContent(JsonSerializer.Serialize(fundInfo), System.Text.Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        IgnoreNullValues = true,
                        PropertyNameCaseInsensitive = true
                    };
                    result = JsonSerializer.Deserialize<bool>(content, options);
                }
                else
                {
                    throw new CustomException(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (CustomException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }
}
