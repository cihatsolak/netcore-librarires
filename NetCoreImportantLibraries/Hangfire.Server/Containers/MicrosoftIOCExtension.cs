using Hangfire.Server.Services.Emails;
using Hangfire.Server.Services.Logs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLibrary.Core.Settings;
using NetCoreLibrary.Data;

namespace Hangfire.Server.Containers
{
    public static class MicrosoftIOCExtension
    {
        public static void CustomConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HangfireDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });

            services.AddControllers();

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ILogService, LogManager>();

            services.Configure<SendGridSettings>(configuration.GetSection(nameof(SendGridSettings)));
        }
    }
}
