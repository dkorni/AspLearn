using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace UniteConfiguration
{
    public class ConfigMiddleware
    {
        private RequestDelegate _next;

        public ConfigMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            await context.Response.WriteAsync($"name: {configuration["name"]} age: {configuration["age"]}");
        }
    }
}
