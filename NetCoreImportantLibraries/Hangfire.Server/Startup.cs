using Hangfire.Server.Containers;
using Hangfire.Server.Filters;
using Hangfire.Server.Middlewares;
using Hangfire.Server.Shedules;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                    // Veritaban�nda tablolar�n yarat�l�p yarat�lmayaca�� bilgisi
                    // Manuel migration i�lemleri i�in false yap�labilir
                    // Default de�eri true
                    PrepareSchemaIfNecessary = true,

                    // Hangfire'�n ne kadar s�re aral�kta kontrol edece�i bilgisi
                    // Default de�eri 15 saniye
                    QueuePollInterval = TimeSpan.FromMinutes(2),

                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5), //Komut Toplu Maksimum Zaman A��m�
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5), //Kayan G�r�nmezlik Zaman A��m�
                    UseRecommendedIsolationLevel = true, //�nerilen �zolasyon Seviyesini Kullan�n
                    UsePageLocksOnDequeue = true, //S�radan ��kar�rken Sayfa Kilitlerini Kullan
                    DisableGlobalLocks = true //Global Kilitleri Devre D��� B�rak
                };

                config.UseSqlServerStorage(Configuration.GetConnectionString("Default"), options).WithJobExpirationTimeout(TimeSpan.FromHours(6));
            });

            services.AddHangfireServer();


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //www.mysite.com/hangfire
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                DashboardTitle = "Cihat SOLAK Hangfire Dashboard", //Dashboard ekran�ndaki ba�l�k alan�.
                AppPath = "https://github.com/cihatsolak", //Dashboard �zerindeki "back to site" butonu,
                Authorization = new[] { new HangfireDashboardAuthorizationFilter() } //G�venlik i�in authorization i�lemleri
            });

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                /*
                 * Hangfire server, planlanan i�leri s�ralar�na g�re s�ralamak i�in zamanlamay� d�zenli olarak denetler ve �al��anlar�n bunlar� y�r�tmesine olanak tan�r.
                 * Varsay�lan olarak, kontrol aral��� 15 saniyeye e�ittir ancak SchedulePollingInterval �zelli�ini ayarlayarak de�i�tirebiliriz.
                 */

                SchedulePollingInterval = TimeSpan.FromMinutes(1), //1 dakikada 1 kez kontrol et ve g�revleri s�rala g�revleri s�rala.
                WorkerCount = Environment.ProcessorCount * 5 //Arka planda �al��acak job say�s�n� ifade eder.
            });

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 7 }); //Global seviyede bir job hata al�rsa 7 kere tekrarlamas�n� s�yledik.

            //Recurring Jobs
            RecurringJobs.DeleteLogJob();


            app.CustomConfigure();
        }
    }
}
