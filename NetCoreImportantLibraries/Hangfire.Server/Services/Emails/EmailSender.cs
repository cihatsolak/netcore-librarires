using Microsoft.Extensions.Options;
using NetCoreLibrary.Core.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Hangfire.Server.Services.Emails
{
    public partial class EmailSender : IEmailSender
    {
        private readonly SendGridSettings _sendGridSettings;
        public EmailSender(IOptions<SendGridSettings> sendGridSettings)
        {
            _sendGridSettings = sendGridSettings.Value;
        }

        public async Task SenderAsync(string userId, string message)
        {
            var client = new SendGridClient(_sendGridSettings.ApiKey);
            var from = new EmailAddress("dociyik310@gridmire.com", "Example User");
            var subject = "Site bilgilendirme";
            var to = new EmailAddress("cihatsolak@hotmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            await client.SendEmailAsync(msg);
        }
    }
}
