using Npv_Exercise.Core.Constants;
using Npv_Exercise.Core.Models.ActionResults;
using Npv_Exercise.Core.Models.Errors;
using System;
using System.Collections.Generic;

namespace Npv_Exercise.Service.Services.NPV
{
    public class NpvService : INpvService
    {
        public NpvComputationResult ComputeNPV(decimal initialValue, decimal rate, List<decimal> cashflows)
        {            
            var result = new NpvComputationResult
            {
                InitialValue = initialValue,
                Rate = rate,
                Cashflows = cashflows
            };

            try
            {
                // validate inputs first
                result.Errors.AddRange(ValidateNPVInputs(initialValue, rate, cashflows));

                if (!result.HasErrors)
                {
                    for (int ctr = 0; ctr < cashflows.Count; ctr++)
                    {
                        //Formula for NPV https://cleartax.in/s/npv-net-present-value
                        //NPV = (Cash flows)/ (1 + r)^i
                        //i - Initial Investment
                        //Cash flows = Cash flows in the time period
                        //r = Discount rate
                        //i = time period

                        result.PresentValueOfExpectedCashflows += cashflows[ctr] / (decimal)Math.Pow((double)(1 + (rate / 100)), (double)(ctr + 1));
                    }
                }                
            }
            catch(Exception ex)
            {
                // do logging here
                result.Errors.Add(new Error(ErrorCodes.NPVServiceError005));
            }

            return result;
        }

        public ActionResult<List<NpvComputationResult>> ComputeNPVs(decimal initialValue, decimal lowerBoundRate, decimal upperBoundRate, decimal increment, List<decimal> cashflows)
        {
            var result = new ActionResult<List<NpvComputationResult>>
            {
                Data = new List<NpvComputationResult>()
            };

            try
            {
                // validate inputs first
                if(upperBoundRate < lowerBoundRate && upperBoundRate > 100)
                {
                    result.Errors.Add(new Error(
                       ErrorCodes.NPVServiceError006,
                       "The upperbound rate should be greater than the lower bound rate and should be less than or equal to 100"
                   ));
                }
                else
                {
                    var currentRate = lowerBoundRate;
                    while (currentRate <= upperBoundRate)
                    {
                        result.Data.Add(ComputeNPV(initialValue, currentRate, cashflows));
                        currentRate += increment;
                    }
                }
            }
            catch (Exception ex)
            {
                // do logging here
                result.Errors.Add(new Error(ErrorCodes.NPVServiceError007));
            }

            return result;
        }

        public List<Error> ValidateNPVInputs(decimal initialValue, decimal rate, List<decimal> cashflows, decimal? increment = null)
        {
            var errors = new List<Error>();

            if (initialValue > 0)
            {
                // error message is taken from https://www.investopedia.com/calculator/netpresentvalue.aspx
                errors.Add(new Error(
                    ErrorCodes.NPVServiceError001,
                    "In the calculation for NPV, the figure for initial cost must be represented by a negative number because, as a cash outflow, the initial cost is deducted from the present value of future cash inflows."
                ));
            }

            if (rate < 0 || rate > 100)
            {
                // error message is taken from https://www.investopedia.com/calculator/netpresentvalue.aspx                
                errors.Add(new Error(
                    ErrorCodes.NPVServiceError002,
                    "Discount Rate must be greater than 0 and less than or equal to 100."
                ));
            }

            if(increment.HasValue && increment < 0)
            {
                errors.Add(new Error(
                    ErrorCodes.NPVServiceError003,
                    "Increments should be greater than 0"
                ));
            }

            if (cashflows == null || cashflows.Count == 0)
            {
                errors.Add(new Error(
                    ErrorCodes.NPVServiceError004,
                    "Atleast 1 cashflow is required"
                ));
            }

            return errors;
        }
    }
}
