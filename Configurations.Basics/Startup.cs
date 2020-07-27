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

namespace Configurations.Basics
{
    public class Startup
    {
        public IConfiguration AppConfiguration { get; set; }

        public Startup()
        {
            // usually, configuration is initialized in startup class

            // config builder
            var builder = new ConfigurationBuilder()
                // add configuration to memory
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    ["firstname"] = "Tom",
                    ["age"] = "31"
                });

            // crate configuration
            AppConfiguration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // we can dynamically add new or change configuration
            AppConfiguration["firstname"] = "Alex";
            AppConfiguration["status"] = "Married";

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync(AppConfiguration["firstname"]+ " "+AppConfiguration["status"]);
                });
            });
        }
    }
}
