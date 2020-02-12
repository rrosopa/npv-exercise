using Npv_Exercise.Core.Models.NpvVariables;
using System.Collections.Generic;

namespace Npv_Exercise.Core.Models.ActionResults
{
    public class NpvComputationResult : BaseResult
    {
        public decimal NetPresentValue => InitialValue + PresentValueOfExpectedCashflows;
        public decimal PresentValueOfExpectedCashflows { get; set; }

        public decimal InitialValue { get; set; }
        public decimal Rate { get; set; }                

        public List<decimal> Cashflows { get; set; }
    }    
}
