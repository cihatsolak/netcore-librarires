using FluentValidation.AspNetCore;
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

        public static void AddFluentValidationConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews().AddFluentValidation(options =>
            {
                //Startup assemblyine bulunan IValidator interfacesinden miras almış classları servis olarak otomatik ekle
                //Proje içerisindeki tüm IValidator olan classları birer servis olarak kaydedecektir.
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
        }
    }
}
