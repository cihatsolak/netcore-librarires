﻿using Hangfire.Server.Services.Emails;
using System;

namespace Hangfire.Server.Shedules
{
    /// <summary>
    /// Bir kez ve belirlenen süre sonrasında çalışan job tipidir.
    /// Örnek Senayo : Kullanıcı giriş yaptıktan 30 dakika sonra mail gönder
    /// Kullanım: BackgroundJob.Schedule
    /// </summary>
    public class DelayedJobs
    {
        /*
         * Globalde hangfire de job çalışırken hata alırsa 7 kere tekrar dener.
         * fakat bu metot özelinde Attribute kullanarak sadece bu metota özel hata alırsa 3 kere dene diyorum.
         */
        [AutomaticRetry(Attempts = 3)]
        public static string EmailSendToUserJobAfterCertainTime(string userId, string message, int scheduleTime = 15)
        {
            string jobId = BackgroundJob.Schedule<IEmailSender>(emailSender => emailSender.SenderAsync(userId, message), TimeSpan.FromSeconds(scheduleTime));
            return jobId;
        }
    }
}
