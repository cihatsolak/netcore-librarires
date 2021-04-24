﻿using AspNetCoreRateLimit;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLibrary.Data;
using NetCoreLibrary.Web.Filters;
using NetCoreLibrary.Web.Models;
using Smidge;
using System.Linq;

namespace NetCoreLibrary.Web.Infrastructure.IOC.Containers
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

        public static void AddFluentValidationConfiguration(this IServiceCollection services)
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
            //FluentValidation filter -> Note: proje içerisnde filter ile işlem yapmadım. Örnek olması adına koydum.
            services.AddScoped(typeof(ValidationFilter));
        }

        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            //Startup class'ının sahip oldugu Assembly'i(yani projeyi) tara ve tüm dönüşüm işlemlerini gerçekleştireyim.
            services.AddAutoMapper(typeof(Startup));
        }

        public static void AddIPRateLimitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache(); //Gelen requestlerin sayısını memory'de tutacağım.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Gelen request'in header'ındaki verileri okumak için

            //Buradaki class'lar nuget paket aracılığıyla kurduğumuz paket içerisinden gelmekte.
            services.Configure<IpRateLimitOptions>(configuration.GetSection(nameof(IpRateLimitOptions)));
            services.Configure<IpRateLimitPolicies>(configuration.GetSection(nameof(IpRateLimitPolicies)));

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>(); //(Private Cache)
                                                                               //services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>(); //Redis vb (Distributed Cache).

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>(); //(Private Cache)
                                                                                               // services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>(); //Redis vb (Distributed Cache).
        }

        public static void AddSmidgeConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSmidge(configuration.GetSection("SmidgeSettings"));
        }
    }
}
