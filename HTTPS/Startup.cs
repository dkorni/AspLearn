using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HTTPS
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // configure https redirection
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 44344;
            });

            services.AddHsts(options =>
            {
                options.Preload = true;// если этот параметр установлен, то будет использоваться специальный preload-список доменов,
                                       // обращение к которым безопасно.

                                       options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(60);
                options.ExcludedHosts.Add("us.example.com");
                options.ExcludedHosts.Add("www.example.com");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // enable sending to client HTTP Strict Transport Security Protocol header, to activate https
                // this is not redirection
                // Якщо безпека з'єднання https не може бути перевірена (зокрема, якщо TLS-сертифікат сервера
                // не підписано довіреною ключем),
                // буде показано повідомлення про помилку, і користувач буде позбавлений доступу до сайту.
                app.UseHsts();
            }

            // redirect to https
            // when we redirect, before client send message by https in anyway
            // it could cause attacks
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
