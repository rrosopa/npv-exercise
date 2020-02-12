using Npv_Exercise.Core.Models.ActionResults;
using Npv_Exercise.Core.Models.NpvVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Npv_Exercise.Service.Services.NpvVariables
{
    public interface INpvVariableService
    {
        Task<AddResult<NpvVariable>> AddNpvVariable(NpvVariable data);
        Task<FetchResult<NpvVariable>> GetNpvVariableById(int id);
        Task<FetchResult<List<NpvVariable>>> GetNpvVariablesList();
    }
}
