using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLibrary.Data;
using NetCoreLibrary.Web.Filters;
using NetCoreLibrary.Web.Models;
using System.Linq;

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
            services.AddControllersWithViews(options =>
            {
                //options.Filters.Add(new ValidationFilter()); -> ModelState.IsValid olayını bir filter aracılığı ile gerçekleştirmek istediğimizde kulllanırız.
            })
            .AddFluentValidation(options =>
            {
                //Startup assemblyine bulunan IValidator interfacesinden miras almış classları servis olarak otomatik ekle
                //Proje içerisindeki tüm IValidator olan classları birer servis olarak kaydedecektir.
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                // options.SuppressModelStateInvalidFilter = true; -> ModelState Invalid oldugunda da gelen request'i kabul et 

                //ModelState Invalid olduğunda döneceğim responsemodel
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponseModel = new ErrorResponseModel
                    {
                        Status = 400,
                        Errors = context.ModelState.Values.Where(p => p.Errors.Count > 0).SelectMany(p => p.Errors).Select(p => p.ErrorMessage).ToList()
                    };

                    return new BadRequestObjectResult(errorResponseModel);
                };
            });
        }

        public static void AddFilterConfiguration(this IServiceCollection services)
        {
            services.AddScoped(typeof(ValidationFilter));
        }
    }
}
