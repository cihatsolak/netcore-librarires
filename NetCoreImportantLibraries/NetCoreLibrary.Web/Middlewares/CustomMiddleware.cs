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

            });
        }
    }
}
