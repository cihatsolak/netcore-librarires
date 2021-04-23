using Hangfire.Server.Managers.ContinuationJobs;

namespace Hangfire.Server.Shedules
{
    public class ContinuationJobs
    {
        /// <summary>
        /// Log silindiğinde bildirim at.
        /// </summary>
        /// <param name="parentId">Kendisinden önce çalışmış job id</param>
        /// <param name="message">Örnek mesaj</param>
        public static void ReportDeleteLogStatus(string parentId, string message)
        {
            BackgroundJob.ContinueJobWith<DeleteLogStatusManager>(parentId, manager => manager.ReportDeleteLogStatusAsync(message));
        }
    }
}
