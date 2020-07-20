using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Services.ConfigureServices
{
    public class Startup
    {
        private IServiceCollection _services;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _services = services;

            // after it we can us MVC service in configure method
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
                   var sb = new StringBuilder();
                   sb.Append("<table>");
                   sb.Append("<tr><th>Тип</th><th>Lifetime</th><th>Реализация</th></tr>");

                   foreach (var svc in _services)
                   {
                       sb.Append("<tr>");
                       sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                       sb.Append($"<td>{svc.Lifetime}</td>");
                       sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                       sb.Append("</tr>");
                   }

                   sb.Append("</table>");
                   context.Response.ContentType = "text/html;charset=utf-8";
                   await context.Response.WriteAsync(sb.ToString());
                });
            });
        }
    }
}
