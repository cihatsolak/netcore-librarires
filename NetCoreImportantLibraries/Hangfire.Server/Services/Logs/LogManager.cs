using Microsoft.EntityFrameworkCore;
using NetCoreLibrary.Data;
using System;
using System.Threading.Tasks;

namespace Hangfire.Server.Services.Logs
{
    public class LogManager : ILogService
    {
        private readonly HangfireDbContext _hangfireDbContext;

        public LogManager(HangfireDbContext hangfireDbContext)
        {
            _hangfireDbContext = hangfireDbContext;
        }

        public async Task DeleteLogAsync(int day)
        {
            string query = string.Format("DELETE FROM [LOG] WHERE CreatedOn <= '{0}'", DateTime.Now.AddDays(day * -1).ToString("yyyy-MM-dd"));
            await _hangfireDbContext.Database.ExecuteSqlRawAsync(query);
        }
    }
}
