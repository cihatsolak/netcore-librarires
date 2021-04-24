using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetCoreLibrary.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost webHost = CreateHostBuilder(args).Build();

            // IpPolicyStore �rne�ini al�n
            var ipPolicyStore = webHost.Services.GetRequiredService<IIpPolicyStore>();

            ipPolicyStore.SeedAsync().Wait();

            webHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                //logging.ClearProviders(); -> Default gelen ILogger mekanizmas�n� devre d��� b�rakmak i�in
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
