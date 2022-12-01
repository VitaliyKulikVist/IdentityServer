// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    /// <summary>
    /// <seealso cref="https://identityserver4.readthedocs.io/en/latest/quickstarts/1_client_credentials.html">
    /// Protecting an API using Client Credentials</seealso> 
    /// </summary>
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment environment)
        {
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Додає служби для контролерів до вказаного IServiceCollection Цей метод не зареєструє служби, які використовуються для сторінок.
            services.AddControllersWithViews();

            var builder = services.AddIdentityServer(options =>
            {
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                //Видає претензію aud із форматом видавця/ресурсів. Це потрібно для деяких старих систем перевірки маркерів доступу. За замовчуванням значення false.
                options.EmitStaticAudienceClaim = true;
            })
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                //Захоплює синхронні та асинхронні екземпляри System.Exception із конвеєра та генерує відповіді на помилки HTML
                app.UseDeveloperExceptionPage();
            }

            // Вмикає обслуговування статичних файлів для поточного шляху запиту
            app.UseStaticFiles();
            //Додає проміжне програмне забезпечення Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware до зазначеного IApplicationBuilder
            app.UseRouting();
            //Додає IdentityServer до конвеєра
            app.UseIdentityServer();

            // Додає AuthorizationMiddleware до зазначеного IApplicationBuilder, що вмикає можливості авторизації
            app.UseAuthorization();
            //Додає проміжне програмне забезпечення EndpointMiddleware до вказаного IApplicationBuilder з примірниками EndpointDataSource, створеними з налаштованого Microsoft.AspNetCore.Routing.IEndpointRouteBuilder. EndpointMiddleware виконає Microsoft.AspNetCore.Http.Endpoint, пов’язаний із поточним запитом
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
