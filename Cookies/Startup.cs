using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cookies
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
           app.Run(async (context) =>
           {
               // check that cookie is setup
               if (context.Request.Cookies.ContainsKey("name"))
               {
                   string name = context.Request.Cookies["name"];
                   await context.Response.WriteAsync($"Hello {name}");
               }
               else
               {
                   // add cookie
                   context.Response.Cookies.Append("name", "Tom");
                   await context.Response.WriteAsync("Hello world!");
               }
           });
        }
    }
}
