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
            services.AddSwaggerConfiguration();

            services.AddDatabaseDeveloperPageExceptionFilter();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) //Development ortam�ndaysam?
            {
                app.UseDeveloperExceptionPage(); //Hata al�nd��� zaman hata sayfas�n� g�ster
                app.UseStatusCodePages("text/plain", "Hata durum Kodu: {0}"); //Herhangi bir hata da durum kodu g�r�nt�ler
                app.UseDeveloperExceptionPage(); //Ef ile veritaban� hatas� ald���m�zda
                app.UseMigrationsEndPoint(); //Ef ile veritaban� hatas� ald���m�zda
            }
            else //Production, Staging
            {
                app.UseExceptionHandler("/Home/Error"); //Hata sayfas�na y�nlendir.
            }

            if (env.IsEnvironment("Alfa")) //Custom environment yazd�g�m�zda
            {

            }

            app.UseHttpsRedirection(); //Http'den gelen istekleri https'e y�nlendir.
            app.UseHsts(); //Taray�c�lara https �zerinden istek atmaya ikna et

            app.UseIpRateLimiting(); //K�t�phane taraf�ndan gelen middleware
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.CustomUseSmidge(); //Custom Smidge Middleware

            app.CustomUseSwagger(); //Custom Swagger Middleware

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
