using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCoreLibrary.Web.Infrastructure.IOC.Containers;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbConfiguration(Configuration);
            services.AddFluentValidationConfiguration();
            services.AddFilterConfiguration();
            services.AddAutoMapperConfiguration();
            services.AddIPRateLimitConfiguration(Configuration);
            services.AddSmidgeConfiguration(Configuration);
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //Development ortamýndaysam?
            {
                app.UseDeveloperExceptionPage(); //Hata alýndýðý zaman hata sayfasýný göster
                
                //1.Yol
                app.UseStatusCodePages("text/plain", "Hata durum Kodu: {0}"); //Herhangi bir hata da durum kodu görüntüler
            }
            else //Production, Staging
            {
                app.UseExceptionHandler("/Home/Error"); //Hata sayfasýna yönlendir.
            }

            app.UseHttpsRedirection(); //Http'den gelen istekleri https'e yönlendir.
            app.UseHsts(); //Tarayýcýlara https üzerinden istek atmaya ikna et

            app.UseIpRateLimiting(); //Kütüphane tarafýndan gelen middleware

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.CustomUseSmidge(); //Custom Smidge Middleware

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
