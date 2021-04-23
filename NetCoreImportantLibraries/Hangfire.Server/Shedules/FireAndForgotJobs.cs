﻿using Hangfire.Server.Services.Emails;

namespace Hangfire.Server.Shedules
{
    /// <summary>
    /// Bir kere ve hemen çalışan job tipidir. İş tanımlıdır ve ardından bir kere tetiklenir.
    /// Örnek Senayo : Bilgi maili gönderimi
    /// Kullanım: BackgroundJob.Enqueue
    /// </summary>
    public partial class FireAndForgotJobs
    {
        /*
         * Globalde hangfire de job çalışırken hata alırsa 10 kere tekrar dener.
         * fakat bu metot özelinde Attribute kullanarak sadece bu metota özel hata alırsa 3 kere dene diyorum.
         */
        [AutomaticRetry(Attempts = 3)]
        public static void EmailSendToUserJob(string userId, string message)
        {
            BackgroundJob.Enqueue<IEmailSender>(src => src.SenderAsync(userId, message));
        }
    }
}
