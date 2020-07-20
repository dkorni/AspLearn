using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.CreateOwnServices.extensions;
using Services.CreateOwnServices.services;

namespace Services.CreateOwnServices
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // add own custom service to container
            services.AddTransient<IMessageSender, SmsMessageSender>();
            // services.AddTransient<TimeService>();

            // after we can use transient in any part of our app

            // here we use own extension method
            services.AddTimeService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // system auto pass to IMessageSender instance of SmsMessageSender instance
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageSender messageSender, TimeService timeService)
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
                    await context.Response.WriteAsync(messageSender.Send()+"\n"+timeService.GetTime());
                });
            });
        }
    }
}
