using Microsoft.AspNetCore.Http;
using Services.CreateOwnServices.services;
using System.Threading.Tasks;

namespace Services.PassDependencies.middleware
{
    public class TimeMiddleware
    {
        private readonly RequestDelegate _next;

        public TimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TimeService timeService)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(timeService.GetTime());
        }
    }
}
