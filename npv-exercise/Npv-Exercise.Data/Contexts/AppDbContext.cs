using Microsoft.EntityFrameworkCore;

namespace Npv_Exercise.Data.Contexts
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // mappings here
        }
    }
}
