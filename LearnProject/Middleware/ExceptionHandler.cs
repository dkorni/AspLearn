using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LearnProject.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);

            if (context.Response.StatusCode == 403)
            {
                context.Response.WriteAsync("Access denied, LOH!");
            }

            if (context.Response.StatusCode == 404)
            {
                context.Response.WriteAsync("Page wasn't found, LOH!");
            }
        }
    }
}
