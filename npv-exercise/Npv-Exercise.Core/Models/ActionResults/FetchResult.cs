namespace Npv_Exercise.Core.Models.ActionResults
{
    public class FetchResult<T> : BaseResult
    {
        public T Data { get; set; }
    }
}
