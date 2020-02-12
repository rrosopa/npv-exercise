using Microsoft.EntityFrameworkCore;
using Npv_Exercise.Data.Entities.NpvVariables;

namespace Npv_Exercise.Data.Contexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<NpvVariableEntity> NpvVariables { get; set; }
        public DbSet<NpvVariableCashflowEntity> NpvVariableCashflows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NpvVariableEntityMapping());
            modelBuilder.ApplyConfiguration(new NpvVariableCashflowEntityMapping());
        }
    }
}
