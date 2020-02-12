using Npv_Exercise.Core.Models.ActionResults;
using Npv_Exercise.Core.Models.Errors;
using System.Collections.Generic;

namespace Npv_Exercise.Service.Services.NPV
{
    public interface INpvService
    {
        NpvComputationResult ComputeNPV(decimal initialValue, decimal rate, List<decimal> cashflows);

        ActionResult<List<NpvComputationResult>> ComputeNPVs(decimal initialValue, decimal lowerBoundRate, decimal upperBoundRate, decimal increment, List<decimal> cashflows);

        List<Error> ValidateNPVInputs(decimal initialValue, decimal rate, List<decimal> cashflows, decimal? increment = null);
    }
}
