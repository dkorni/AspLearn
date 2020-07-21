using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Services.Singletone.Scope.Services;

namespace Services.Singletone.Scope.Middleware
{
    public class TimerMiddleware
    {
        private readonly RequestDelegate _next;
        private TimeService _timeService;

        public TimerMiddleware(RequestDelegate next, TimeService timeService)
        {
            _next = next;
            _timeService = timeService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync(_timeService.GetTime());
        }
    }
}
