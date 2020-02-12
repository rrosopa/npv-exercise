using Microsoft.EntityFrameworkCore;
using Npv_Exercise.Data.Entities.NpvVariables;

namespace Npv_Exercise.Data.Contexts
{
    public interface IAppDbContext
    {
        DbSet<NpvVariableEntity> NpvVariables { get; set; }
        DbSet<NpvVariableCashflowEntity> NpvVariableCashflows { get; set; }

        int SaveChanges();
    }
}
