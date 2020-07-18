using LearnProject.Midleware;
using Microsoft.AspNetCore.Builder;

namespace LearnProject
{
    public static class Extensions
    {
        // определяем метод расширения для IApplicationBuilder, использовании тукенов 
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder, string pattern)
        {
            // добавляем свой компонент 
            return builder.UseMiddleware<TokenMiddleware>(pattern);
        }
    }
}