using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Singletone.Scope.Middleware;
using Services.Singletone.Scope.Services;

namespace Services.Singletone.Scope
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddTransient<ITimer, Timer>();

            // singleton objects like middleware cannot take scope objects 
            // during dependency injection
            // because middleware is created at start of application,
            // but scoped object each http request
            // services.AddScoped<TimeService>();

            // in this case when time middleware is created, it creates TimeService,
            // but TimeService can't be initialized because of scoped ITimer
            //services.AddScoped<ITimer, Timer>();
            //services.AddTransient<TimeService>();

            services.AddTransient<ITimer, Timer>();
            services.AddTransient<TimeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<TimerMiddleware>();
        }
    }
}
