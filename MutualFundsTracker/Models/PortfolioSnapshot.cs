namespace MutualFundsTracker.Models
{
    public class PortfolioSnapshot
    {
        public int FundId { get; set; }

        public long PortfolioId { get; set; }

        public string FundName { get; set; }

        public double InvestmentValue { get; set; }

        public double CurrentValue { get; set; }

        public double Return { get; set; }
    }
}
