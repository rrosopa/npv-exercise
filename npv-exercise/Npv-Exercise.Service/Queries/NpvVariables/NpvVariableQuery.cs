using Microsoft.EntityFrameworkCore;
using Npv_Exercise.Core.Models.NpvVariables;
using Npv_Exercise.Data.Entities.NpvVariables;
using Npv_Exercise.Data.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Npv_Exercise.Service.Queries.NpvVariables
{
    internal static class NpvVariableQuery
    {
        public static Task<List<NpvVariable>> GetNpvVariables(this IQueryable<NpvVariableEntity> npvVariables)
        {
            return npvVariables
                    .Select(x => new NpvVariable
                    {
                        Id = x.Id,
                        InitialValue = x.InitialValue,
                        LowerBoundRate = x.LowerBoundRate,
                        UpperBoundRate = x.UpperBoundRate,
                        Increment = x.Increment,
                        Cashflows = x.CashflowEntities
                            .OrderBy(cashflow => cashflow.Order)
                            .Select(cashflow => new NpvVariableCashflow
                            {
                                Id = cashflow.Id,
                                NpvVariableId = cashflow.NpvVariableId,
                                Cashflow = cashflow.Cashflow,
                                Order = cashflow.Order
                            }).ToList()
                    })
                    .ToListAsync();
        }

        public async static Task<NpvVariable> GetNpvVariableById(this IQueryable<NpvVariableEntity> npvVariables, int id)
        {
            return (await npvVariables.FilterById(id).GetNpvVariables()).SingleOrDefault();                    
        }
    }
}
