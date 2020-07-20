using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.CreateOwnServices.services;
using Services.PassDependencies.middleware;

namespace Services.PassDependencies
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMessageSender, SmsMessageSender>();
            services.AddTransient<MessageService>();

            services.AddTransient<TimeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // get directly instance of IMessageSender, we can use it oly in startup class
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MessageService service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    // await context.Response.WriteAsync(service.Send());

                    // get instance by app.ApplicationServices.GetService()
                    await context.Response.WriteAsync(app.ApplicationServices.GetService<IMessageSender>().Send());
                });
            });

            app.Map("/time", GetTime);

            //app.Run(async (context) =>
            //{
            //    // get instance by context.RequestServices.GetService()
            //    var ser = context.RequestServices.GetService<IMessageSender>();
            //    await context.Response.WriteAsync(ser.Send());
            //});
        }

        private void GetTime(IApplicationBuilder app)
        {
            app.UseMiddleware<TimeMiddleware>();
        }
    }
}
