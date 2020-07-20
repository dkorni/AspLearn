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
using Services.LifeCircle.Middleware;
using Services.LifeCircle.Services;

namespace Services.LifeCircle
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // create each time new instance of ICounter
            services.AddTransient<ICounter, RandomCounter>();

            // create one new instance of ICounter for one http request
            // services.AddScoped<ICounter, RandomCounter>();

            // create one new instance of ICounter for all life time of app
            //services.AddSingleton<ICounter, RandomCounter>();
            services.AddTransient<CounterService>();

            // other singleton implementation 
            //var counter = new RandomCounter();
            //services.AddSingleton<ICounter>(counter);
            //services.AddSingleton(new CounterService(counter));

            // use factory
            services.AddTransient<IMessageSender>(provider => {
                if (DateTime.Now.Hour > 12)
                {
                    return new EmailMessageSender();
                }
                else
                {
                    return new SmsMessageSender();
                }
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageSender sender)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Map("/message", (app) => app.Run(async (context) => await context.Response.WriteAsync(sender.Send())));

            app.UseMiddleware<CounterMiddleware>();
        }
    }
}
