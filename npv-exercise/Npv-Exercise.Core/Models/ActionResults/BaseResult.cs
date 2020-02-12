using Npv_Exercise.Core.Models.Errors;
using System.Collections.Generic;
using System.Linq;

namespace Npv_Exercise.Core.Models.ActionResults
{
    public abstract class BaseResult
    {
        public BaseResult()
        {
            Errors = new List<Error>();
        }

        public List<Error> Errors { get; set; }
        public bool HasErrors => Errors.Any();
    }
}
