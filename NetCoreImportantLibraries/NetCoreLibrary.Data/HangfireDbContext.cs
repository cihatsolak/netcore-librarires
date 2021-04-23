using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Core.Domain;

namespace NetCoreLibrary.Data
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().HasKey(p => p.Id);
        }
    }
}
