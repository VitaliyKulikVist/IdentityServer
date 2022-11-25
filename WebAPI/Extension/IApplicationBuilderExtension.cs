using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebAPI.Extension
{
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder AddApplicationBuilderFromAPI(this IApplicationBuilder app, IWebHostEnvironment environment)
        {
            // Налаштуйте конвеєр запитів HTTP.
            if (environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                .RequireAuthorization("ApiScope");
            });

            return app;
        }
    }
}
