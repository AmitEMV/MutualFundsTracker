using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
           var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/home"));
           return await response.Content.ReadAsStringAsync();
        }
    }
}
