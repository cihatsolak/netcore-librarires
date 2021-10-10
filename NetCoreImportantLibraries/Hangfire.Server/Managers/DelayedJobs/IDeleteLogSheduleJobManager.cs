using System.Threading.Tasks;

namespace Hangfire.Server.Managers.DelayedJobs
{
    public interface IDeleteLogSheduleJobManager
    {
        [Queue("general")]
        [AutomaticRetry(Attempts = 0, LogEvents = false, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        Task DeleteLogAsync();
    }
}
