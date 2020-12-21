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

namespace UniteConfiguration
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("conf.json")
                .AddInMemoryCollection(new Dictionary<string, string>()
                {
                    {"name", "Tom" },
                    {"age", "31" }
                })
                .AddConfiguration(configuration); // добавляем конфигурацию по дефолту из зависимостей

            ApplicationConfiguration = builder.Build();
        }

        public IConfiguration ApplicationConfiguration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IConfiguration>(provider => ApplicationConfiguration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMiddleware<ConfigMiddleware>();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        var name = ApplicationConfiguration["name"];
            //        var color = ApplicationConfiguration["color"];
            //        var java = ApplicationConfiguration["JAVA_HOME"];

            //        await context.Response.WriteAsync($"<p style='color:{color}'>{name}</p><br><p>{java}</p>");
            //    });
            //});
        }
    }
}
