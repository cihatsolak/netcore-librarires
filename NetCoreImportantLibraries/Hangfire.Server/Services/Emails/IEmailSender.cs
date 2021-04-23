using System.Threading.Tasks;

namespace Hangfire.Server.Services.Emails
{
    public interface IEmailSender
    {
        Task SenderAsync(string userId, string message);
    }
}
