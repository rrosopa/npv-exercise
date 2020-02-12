namespace Npv_Exercise.Core.Models.ActionResults
{
    public class ActionResult : BaseResult
    {        
    }

    public class ActionResult<T> : BaseResult
    {
        public T Data { get; set; }
    }
}
