using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLibrary.Data;

namespace NetCoreLibrary.Web.Containers
{
    public static class MicrosoftIocExtension
    {
        public static void AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Default"));
            });
        }
    }
}
