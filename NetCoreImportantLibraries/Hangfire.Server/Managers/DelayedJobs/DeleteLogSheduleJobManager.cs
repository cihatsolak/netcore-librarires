using Hangfire.Server.Services.Logs;
using System.Threading.Tasks;

namespace Hangfire.Server.Managers.DelayedJobs
{
    public class DeleteLogSheduleJobManager
    {
        private readonly ILogService _logService;
        public DeleteLogSheduleJobManager(ILogService logService)
        {
            _logService = logService;
        }

        /// <summary>
        /// Her gün belirlediğim saatte veritabanındaki ürünleri silecek.
        /// </summary>
        public async Task DeleteLogAsync()
        {
            await _logService.DeleteLogAsync(20);
        }
    }
}
