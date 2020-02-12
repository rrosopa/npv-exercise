using System.Collections.Generic;

namespace Npv_Exercise.Core.Models.NpvVariables
{
    public class NpvVariable : BaseModel
    {
        public decimal InitialValue { get; set; }
        public decimal LowerBoundRate { get; set; }
        public decimal UpperBoundRate { get; set; }
        public decimal Increment { get; set; }

        public virtual List<NpvVariableCashflow> Cashflows { get; set; }
    }
}
