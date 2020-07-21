using Microsoft.AspNetCore.Http;
using Services.CreateOwnServices.services;
using System.Threading.Tasks;

namespace Services.Middleware.Middleware
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;
        private TimeService _timeService;

        public TimerMiddleware(RequestDelegate next, TimeService timeService)
        {
            _next = next;

            // more usable for singleton services
            _timeService = timeService;
        }

        public async Task InvokeAsync(HttpContext context, TimeService timeService)
        {
            if (context.Request.Path.Value.ToLower() == "/time")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                // await context.Response.WriteAsync($"{_timeService.Time}");
                await context.Response.WriteAsync($"{timeService.Time}");

                // depended on environment 
                //var srv = context.RequestServices.GetService(typeof(TimeService)) as TimeService;
                //await context.Response.WriteAsync(srv?.Time);
            }
            else
            {
                _next.Invoke(context);
            }
        }
    }
}
