using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LearnProject.Midleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _pattern;

        // Класс middleware должен иметь конструктор, который принимает
        // параметр типа RequestDelegate. Через этот параметр можно получить ссылку на тот делегат запроса,
        // который стоит следующим в конвейере обработки запроса
        public TokenMiddleware(RequestDelegate next, string pattern)
        {
            this._next = next;
            _pattern = pattern;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (token != _pattern)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Token is invalid");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }
}
