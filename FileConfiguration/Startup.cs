using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FileConfiguration
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup()
        {
            // when 2 files have same keys, last will be picked out
            var builder = new ConfigurationBuilder().AddJsonFile("Config/conf.json")
                .AddJsonFile("Config/settings.json");
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
                    await context.Response.WriteAsync($"<p1 style='color:{Configuration["color"]};'>{Configuration["text"]}</p1>\n");

                    // display hierarchy option 
                    await context.Response.WriteAsync(Configuration["namespace:class:method"]);
                });
            });
        }
    }
}
