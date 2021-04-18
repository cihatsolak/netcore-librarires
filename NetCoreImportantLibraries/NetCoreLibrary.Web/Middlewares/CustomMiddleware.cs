using Microsoft.AspNetCore.Builder;
using Smidge;

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

                //eğer aynı klasör içerisinde birden fazla bundle edilmesi gereken dosya varsa, sadece dosya yolunu vermemiz yeterlidir.
                //bundle.CreateJs("my-js-bundle", "~/js/");
            });
        }
    }
}
