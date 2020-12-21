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

namespace WorkWithConfiguration
{
    public class Startup
    {
        public IConfiguration ApplicationConfiguration;

        public Startup()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("config.json");
            builder.AddJsonFile("project.json");
            ApplicationConfiguration = builder.Build();
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
                    //var database = ApplicationConfiguration.GetSection("ConnectionStrings")
                    //    .GetSection("DefaultConnection").Value;

                    // var database = ApplicationConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value;

                    var database = ApplicationConfiguration.GetConnectionString("DefaultConnection");

                    await context.Response.WriteAsync($"{GetSectionContent(ApplicationConfiguration)}");
                });
            });
        }

        private string GetSectionContent(IConfiguration configuration)
        {
            string sectionContent = "";
            foreach (var section in configuration.GetChildren())
            {
                sectionContent += "\"" + section.Key + "\":";
                if (section.Value == null)
                {
                    string subSection = GetSectionContent(section);
                    sectionContent += "{\n" + subSection + "},\n";
                }
                else
                {
                    sectionContent += section.Value + "\",\n";
                }
            }
            return sectionContent;
        }
    }
}
