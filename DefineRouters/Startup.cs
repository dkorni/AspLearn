using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DefineRouters
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
            var routeHandler = new RouteHandler(Handle);
            var routeBuilder = new RouteBuilder(app, routeHandler);
            routeBuilder.MapRoute("{controller}/{action}",
                async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("Двухсегментный запрос");
                });
            routeBuilder.MapRoute("{controller}/{action}/{id}",
                async context =>
                {
                    context.Response.ContentType = "text/html; charset=utf-8";
                    await context.Response.WriteAsync("Трехсегментный запрос");
                });
            routeBuilder.MapRoute("default", "{controller}/{action?}/{id?}", new {controller="home", action="index"});

            app.UseRouter(routeBuilder.Build());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        private async Task Handle(HttpContext context)
        {
            var par = context.Request.RouteValues;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<table>");

            foreach (var keyValue in par)
            {
                stringBuilder.Append($"<tr><td>{keyValue.Key}</td><td>{keyValue.Value}</td></tr>");
            }
            stringBuilder.Append("</table>");

            await context.Response.WriteAsync(stringBuilder.ToString());
        }
    }
}
