namespace Npv_Exercise.Core.Models.NpvVariables
{
    public class NpvVariableCashflow : BaseModel
    {
        public int NpvVariableId { get; set; }
        public decimal Cashflow { get; set; }
        public int Order { get; set; }
    }
}
