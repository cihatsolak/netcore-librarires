using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Core.Domain;

namespace NetCoreLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
