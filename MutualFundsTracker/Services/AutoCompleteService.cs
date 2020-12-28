using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MutualFundsTracker.Services
{
    public class AutoCompleteService
    {
        private readonly string apiURL;
        static readonly HttpClient httpClient = new HttpClient();

        public AutoCompleteService(string URL)
        {
            apiURL = URL;
        }

        public async Task<List<string>> GetSuggestions()
        {
            var autoCompleteList = new List<string>();

            try
            {
                var response = await httpClient.GetAsync(new Uri($"{apiURL}/api/autocomplete"));
                var result = await response.Content.ReadAsStringAsync();

                foreach (string str in result.Split(",").ToList())
                {
                    autoCompleteList.Add(str);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);   
            }

            return autoCompleteList;
        }
    }
}
