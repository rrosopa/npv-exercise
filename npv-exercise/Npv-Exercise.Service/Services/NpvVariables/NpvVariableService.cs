using Npv_Exercise.Core.Models.NpvVariables;
using Npv_Exercise.Data.Contexts;
using Npv_Exercise.Service.Queries.NpvVariables;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Npv_Exercise.Service.Services.NpvVariables
{
    public class NpvVariableService : INpvVariableService
    {
        private readonly IAppDbContext _appDbContext;
        public NpvVariableService(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<NpvVariable>> Test()
        {
            return await _appDbContext.NpvVariables.GetNpvVariables();
        }
    }
}
