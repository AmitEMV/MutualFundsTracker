using System;
using System.Text.Json.Serialization;

namespace MutualFundsTracker.Models
{
    public class ValueTrend
    {
        [JsonPropertyName("currentvalue")]
        public double CurrentValue { get; set; }

        [JsonPropertyName("investedvalue")]
        public double InvestedValue { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
