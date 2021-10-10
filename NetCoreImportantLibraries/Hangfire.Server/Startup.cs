using Hangfire.Server.Containers;
using Hangfire.Server.Filters;
using Hangfire.Server.Middlewares;
using Hangfire.Server.Shedules;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLibrary.Core.Settings;
using System;

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

            //hangfire configure


            services.AddHangfire(config =>
            {
                var options = new SqlServerStorageOptions
                {
                    // Veritabanýnda tablolarýn yaratýlýp yaratýlmayacaðý bilgisi
                    // Manuel migration iþlemleri için false yapýlabilir
                    // Default deðeri true
                    PrepareSchemaIfNecessary = true,

                    // Hangfire'ýn ne kadar süre aralýkta kontrol edeceði bilgisi
                    // Default deðeri 15 saniye
                    QueuePollInterval = TimeSpan.FromMinutes(2),

                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5), //Komut Toplu Maksimum Zaman Aþýmý
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5), //Kayan Görünmezlik Zaman Aþýmý
                    UseRecommendedIsolationLevel = true, //Önerilen Ýzolasyon Seviyesini Kullanýn
                    UsePageLocksOnDequeue = true, //Sýradan Çýkarýrken Sayfa Kilitlerini Kullan
                    DisableGlobalLocks = true //Global Kilitleri Devre Dýþý Býrak
                };

                config.UseSqlServerStorage(Configuration.GetConnectionString("Default"), options).WithJobExpirationTimeout(TimeSpan.FromHours(6));
            });

            services.AddHangfireServer();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var hangfireSettings = Configuration.GetSection(nameof(HangfireSettings)).Get<HangfireSettings>();

            //www.mysite.com/hangfire
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Cihat SOLAK Hangfire Dashboard", //Dashboard ekranýndaki baþlýk alaný.
                AppPath = "https://github.com/cihatsolak", //Dashboard üzerindeki "back to site" butonu,
                Authorization = new[] { new HangfireCustomBasicAuthenticationFilter{
                    User = hangfireSettings.UserName,
                    Pass = hangfireSettings.Password
                } } //Güvenlik için authorization iþlemleri
            });

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                /*
                 * Hangfire server, planlanan iþleri sýralarýna göre sýralamak için zamanlamayý düzenli olarak denetler ve çalýþanlarýn bunlarý yürütmesine olanak tanýr.
                 * Varsayýlan olarak, kontrol aralýðý 15 saniyeye eþittir ancak SchedulePollingInterval özelliðini ayarlayarak deðiþtirebiliriz.
                 */

                SchedulePollingInterval = TimeSpan.FromMinutes(1), //1 dakikada 1 kez kontrol et ve görevleri sýrala görevleri sýrala.
                WorkerCount = Environment.ProcessorCount * 5, //Arka planda çalýþacak job sayýsýný ifade eder.
                Queues = new[] { "general" }
            });

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 7 }); //Global seviyede bir job hata alýrsa 7 kere tekrarlamasýný söyledik.

            //Recurring Jobs
            RecurringJobs.DeleteLogJob();


            app.CustomConfigure();
        }
    }
}
