using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ConfigAndIOptions
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PersonMiddleware
    {
        private readonly RequestDelegate _next;

        private Person person;

        public PersonMiddleware(RequestDelegate next, IOptions<Person> personOptions)
        {
            _next = next;
            person = personOptions.Value;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"<p>Name: {person?.Name}</p>");
            stringBuilder.Append($"<p>Age: {person?.Age}</p>");
            stringBuilder.Append($"<p>Company: {person?.Company.Title}</p>");
            stringBuilder.Append($"<h3>Languages</h3><ul>");
            foreach (var language in person.Languages)
            {
                stringBuilder.Append($"<li>{language}</li>");
            }
            stringBuilder.Append($"</ul>");

            await httpContext.Response.WriteAsync(stringBuilder.ToString());
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PersonMiddlewareExtensions
    {
        public static IApplicationBuilder UsePersonMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PersonMiddleware>();
        }
    }
}
