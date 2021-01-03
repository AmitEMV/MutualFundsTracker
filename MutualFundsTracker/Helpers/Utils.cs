using MutualFundsTracker.Models;
using System;
using System.Linq;

namespace MutualFundsTracker.Helpers
{
    public class Utils
    {
        public static FundDistribution[] GetFundDistributionPercentage(FundDistribution[] values)
        {
            FundDistribution[] fundDistributionsinPercent = new FundDistribution[values.Length];

            double totalValue = values.Sum(val => val.FundValue);

            fundDistributionsinPercent = values.Select(value =>
            {
                return new FundDistribution()
                {
                    FundType = value.FundType,
                    FundValue = Math.Round((value.FundValue / totalValue) * 100)
                };
            }).ToArray();

            return fundDistributionsinPercent;
        }
    }
}
