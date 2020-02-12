namespace Npv_Exercise.Core.Models.Errors
{
    public class Error
    {
        public Error() { }
        public Error(string code)
        {
            Code = code;
            Message = "An error occured while processing your request.";
        }
        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }
}
