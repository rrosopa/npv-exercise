namespace Npv_Exercise.Core.Models.ActionResults
{
    public class AddResult<T> : BaseResult
    {
        public T Result { get; set; }
    }
}
