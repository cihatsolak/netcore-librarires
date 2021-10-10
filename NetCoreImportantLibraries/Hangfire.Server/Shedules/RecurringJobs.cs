using Hangfire.Server.Managers.DelayedJobs;
using System;

namespace Hangfire.Server.Shedules
{
    /// <summary>
    /// Periyod bazlı çalışan job türüdür. Günlük/Haftalık/Aylık/Yıllık
    /// Örnek Senayo : Her pazartesi  saat 09:30'da çalışanlara haftanın planını at.
    /// Kullanım: BackgroundJob.Schedule
    /// </summary>
    public class RecurringJobs
    {
        /*
         * Globalde hangfire de job çalışırken hata alırsa 7 kere tekrar dener.
         * fakat bu metot özelinde Attribute kullanarak sadece bu metota özel hata alırsa 3 kere dene diyorum.
         */
        [AutomaticRetry(Attempts = 3)]
        public static void DeleteLogJob()
        {
            RecurringJob.AddOrUpdate<IDeleteLogSheduleJobManager>("Log Silme Görevi", manager => manager.DeleteLogAsync(), "35 15 * * *", TimeZoneInfo.Local);
        }
    }
}
