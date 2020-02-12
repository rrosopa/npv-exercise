using Microsoft.EntityFrameworkCore;
using Npv_Exercise.Data.Entities.NpvVariables;
using System.Threading;
using System.Threading.Tasks;

namespace Npv_Exercise.Data.Contexts
{
    public interface IAppDbContext
    {
        DbSet<NpvVariableEntity> NpvVariables { get; set; }
        DbSet<NpvVariableCashflowEntity> NpvVariableCashflows { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
