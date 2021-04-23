using Hangfire.Server.Containers;
using Hangfire.Server.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.CustomConfigureServices(Configuration);

            services.AddHangfire(config => config.UseSqlServerStorage(Configuration.GetConnectionString("Default")));
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHangfireDashboard("/hangfire"); //www.mysite.com/hangfire

            app.CustomConfigure();
        }
    }
}
