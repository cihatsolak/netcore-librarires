using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreLibrary.Web.Containers;
using NetCoreLibrary.Web.Middlewares;

namespace NetCoreLibrary.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbConfiguration(Configuration);
            services.AddFluentValidationConfiguration();
            services.AddFilterConfiguration();
            services.AddAutoMapperConfiguration();
            services.AddIPRateLimitConfiguration(Configuration);
            services.AddSmidgeConfiguration(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseIpRateLimiting(); //Kütüphane tarafýndan gelen middleware

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.CustomUseSmidge(); //CustomMiddleware

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
