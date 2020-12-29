using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MutualFundsTracker.Services;

namespace MutualFundsTracker
{
    public class Program
    {
        const string API_URL = "https://localhost:44322";
        const string AUTOCOMPLETE_API_URL = "https://localhost:44394";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            
            builder.Services.AddScoped<FundsService>(s =>
            {
                return new FundsService(API_URL);
            });

            builder.Services.AddScoped<AutoCompleteService>(s =>
            {
                return new AutoCompleteService(AUTOCOMPLETE_API_URL);
            });

            builder.Services.AddScoped<Radzen.NotificationService>();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
