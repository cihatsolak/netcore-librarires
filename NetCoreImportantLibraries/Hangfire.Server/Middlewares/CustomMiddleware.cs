using Microsoft.AspNetCore.Builder;

namespace Hangfire.Server.Middlewares
{
    public static class CustomMiddleware
    {
        public static void CustomConfigure(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
