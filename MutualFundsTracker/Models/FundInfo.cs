using System;

namespace MutualFundsTracker.Models
{
    public class FundInfo
    {
        public string FundName { get; set; }
        public string PurchaseType { get; set; } 
        public string InvestmentType { get; set; } 
        public string PlanType { get; set; } 
        public string FundType { get; set; } 
        public string FundCategory { get; set; } 
        public DateTime PurchaseDate { get; set; }
        public string PurchaseAmount { get; set; }
        public string NumberOfUnits { get; set; }
        public string NAV { get; set; }
        public string FundId { get; set; }
    }
}
