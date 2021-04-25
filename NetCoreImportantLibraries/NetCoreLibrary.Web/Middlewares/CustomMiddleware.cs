using Microsoft.AspNetCore.Builder;
using Smidge;
using Smidge.Cache;
using Smidge.Options;

namespace NetCoreLibrary.Web.Middlewares
{
    public static class CustomMiddleware
    {
        public static void CustomUseSmidge(this IApplicationBuilder app)
        {
            //Hangi dosyaları bundle etmek istiyorsam burada belirteceğim.
            app.UseSmidge(bundle =>
            {

                bundle.CreateJs("my-js-bundle", "~/js/site.js", "~/js/site2.js");

                bundle.CreateCss("my-css-bundle", "~/lib/bootstrap/dist/css/bootstrap.css", "~/css/site.css");


                //bundle.CreateJs("my-js-bundle", "~/js/site.js", "~/js/site2.js")
                ////ForDebug -> HTML tarafta debug="true" olan bundle dosyalarım için geçerli olacak kurallar. (Çok uç bir örnek, development ortamda da bundle etmek için yani var olan davranışı değiştirmek)
                //.WithEnvironmentOptions(BundleEnvironmentOptions.Create().ForDebug(builder => builder.EnableCompositeProcessing() //debug="true" oldugunda da bundle(birleştir) et.
                //                                                                                     .EnableFileWatcher() //bundle içerisindeki dosyalarda bir değişiklik olursa sıfırdan oluştur
                //                                                                                     .SetCacheBusterType<AppDomainLifetimeCacheBuster>() //Uygulama her ayağa kalktığında cache sıfırlanacak
                //                                                                                      //enableEtag:veriyi her seferinde cache'den al || cacheControlMaxAge:0 -> gelen datayı memoryde yada fiziksel diskde asla tutma
                //                                                                                     .CacheControlOptions(enableEtag: false, cacheControlMaxAge: 0)) 
                //                                                                                     .Build());


                //eğer aynı klasör içerisinde birden fazla bundle edilmesi gereken dosya varsa, sadece dosya yolunu vermemiz yeterlidir.
                //bundle.CreateJs("my-js-bundle", "~/js/");
            });
        }

        public static void CustomUseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/v1/swagger.json", "Example API");
            });
        }
    }
}
