using System.Threading.Tasks;

namespace Hangfire.Server.Services.Logs
{
    public interface ILogService
    {
        Task DeleteLogAsync(int day);
    }
}
