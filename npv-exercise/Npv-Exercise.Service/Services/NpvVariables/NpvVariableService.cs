using Npv_Exercise.Core.Constants;
using Npv_Exercise.Core.Models.ActionResults;
using Npv_Exercise.Core.Models.Errors;
using Npv_Exercise.Core.Models.NpvVariables;
using Npv_Exercise.Data.Contexts;
using Npv_Exercise.Data.Entities.NpvVariables;
using Npv_Exercise.Service.Queries.NpvVariables;
using System;
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

        public async Task<AddResult<NpvVariable>> AddNpvVariable(NpvVariable data)
        {
            var addResult = new AddResult<NpvVariable>();

            try
            {
                var entry = new NpvVariableEntity
                {
                    InitialValue = data.InitialValue,
                    LowerBoundRate = data.LowerBoundRate,
                    UpperBoundRate = data.UpperBoundRate,
                    Increment = data.Increment,
                    CashflowEntities = new List<NpvVariableCashflowEntity>()
                };

                var order = 1;
                data.Cashflows?.ForEach(x =>
                {
                    entry.CashflowEntities.Add(new NpvVariableCashflowEntity
                    {
                        Cashflow = x.Cashflow,
                        Order = order++
                    });
                });

                _appDbContext.NpvVariables.Add(entry);
                var saveResult = await _appDbContext.SaveChangesAsync();
                if(saveResult == 0)
                {
                    addResult.Errors.Add(new Error(ErrorCodes.NpvVariableServiceError001));
                }
                else
                {
                    addResult.Result = await _appDbContext.NpvVariables.GetNpvVariableById(entry.Id);
                }
            }
            catch(Exception ex)
            {
                // do logging here
                addResult.Errors.Add(new Error(ErrorCodes.NpvVariableServiceError002));
            }

            return addResult;
        }

        public async Task<FetchResult<NpvVariable>> GetNpvVariableById(int id)
        {
            var fetchResult = new FetchResult<NpvVariable>();

            try
            {
                fetchResult.Data = await  _appDbContext.NpvVariables.GetNpvVariableById(id);
                if(fetchResult.Data == null)
                {
                    // do logging here
                    fetchResult.Errors.Add(new Error(ErrorCodes.NpvVariableServiceError005, "NPV Variable does not exists."));
                }
            }
            catch (Exception ex)
            {
                // do logging here
                fetchResult.Errors.Add(new Error(ErrorCodes.NpvVariableServiceError003));
            }

            return fetchResult;
        }

        public async Task<FetchResult<List<NpvVariable>>> GetNpvVariablesList()
        {
            var fetchResult = new FetchResult<List<NpvVariable>>();

            try
            {
                fetchResult.Data = await _appDbContext.NpvVariables.GetNpvVariables();
            }
            catch (Exception ex)
            {
                // do logging here
                fetchResult.Errors.Add(new Error(ErrorCodes.NpvVariableServiceError004));
            }

            return fetchResult;
        }
    }
}
