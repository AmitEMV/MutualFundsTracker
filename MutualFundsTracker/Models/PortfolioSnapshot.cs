namespace MutualFundsTracker.Models
{
    public class PortfolioSnapshot
    {
        public string FundName { get; set; }

        public double InvestmentValue { get; set; }

        public double CurrentValue { get; set; }

        public double Return { get; set; }
    }
}
