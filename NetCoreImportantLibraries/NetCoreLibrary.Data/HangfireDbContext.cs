using Microsoft.EntityFrameworkCore;

namespace NetCoreLibrary.Data
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
