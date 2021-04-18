using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
