using System.Collections.Generic;

namespace npv_exercise.Models
{
    // currently undecided on where to put, this is only used by api controller, it should be put somewhere in this project rather than core...    
    public class NpvCalculateModel
    {
        public decimal InitialValue { get; set; }
        public decimal LowerBoundRate { get; set; }
        public decimal UpperBoundRate { get; set; }
        public decimal Increment { get; set; }

        public List<decimal> Cashflows { get; set; }
    }
}
