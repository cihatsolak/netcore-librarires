using Hangfire.Server.Services.Emails;
using System.Threading.Tasks;

namespace Hangfire.Server.Managers.ContinuationJobs
{
    public class DeleteLogStatusManager
    {
        private readonly IEmailSender _emailSender;

        public DeleteLogStatusManager(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        /*
         * Globalde hangfire de job çalışırken hata alırsa 7 kere tekrar dener.
         * fakat bu metot özelinde Attribute kullanarak sadece bu metota özel hata alırsa 3 kere dene diyorum.
         */
        [AutomaticRetry(Attempts = 3)]
        public async Task ReportDeleteLogStatusAsync(string message)
        {
            await _emailSender.SenderAsync(userId: "1234", message);
        }
    }
}
