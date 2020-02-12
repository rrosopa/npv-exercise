using Npv_Exercise.Core.Models.NpvVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Npv_Exercise.Service.Services.NpvVariables
{
    public interface INpvVariableService
    {
        Task<List<NpvVariable>> Test();
    }
}
