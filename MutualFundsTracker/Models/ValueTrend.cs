using System;
using System.Text.Json.Serialization;

namespace MutualFundsTracker.Models
{
    public class ValueTrend
    {
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
