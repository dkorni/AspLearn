using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ErrorProcessing
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // after it error 500 is thrown - internal server error
            env.EnvironmentName = "Production";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // redirect to exception handler address when some exception occured
                app.UseExceptionHandler("/error");
            }

            app.UseRouting();

            // http error processing default
            // app.UseStatusCodePages();

            // first param - MIME type of response, second - text of message
            //app.UseStatusCodePages("text/plain", "Error. Status code: {0}");

            // redirect http error to specific method
            app.UseStatusCodePagesWithReExecute("/httperror", "?code={0}");

            // map htttperror method
            app.Map("/httperror", ap => ap.Run(async (context) =>
                await context.Response.WriteAsync($"Er: {context.Request.Query["code"]}")));

            app.Map("/error",
                ap => ap.Run(async (context) =>
                    await context.Response.WriteAsync("DevidedByZeroException occured!!!")));

            // simulate exception
            //app.Run(async (context) =>
            //{
            //    int x = 0;
            //    int y = 8 / x;
            //    await context.Response.WriteAsync($"Result = {y}");
            //});

            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/", async (context) => await context.Response.WriteAsync("Hello world!")));

        }
    }
}
