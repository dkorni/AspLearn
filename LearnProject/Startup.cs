using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LearnProject.Middleware;
using LearnProject.Midleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace LearnProject
{
    /// <summary>
    /// Класс Startup является входной точкой в приложение ASP.NET Core.
    /// Этот класс производит конфигурацию приложения, настраивает сервисы, которые приложение будет использовать,
    /// устанавливает компоненты для обработки запроса или middleware.
    /// </summary>
    public class Startup
    {
        private IWebHostEnvironment _environment;

        public Startup(IWebHostEnvironment env)
        {
            _environment = env;
        }

        // При запуске приложения сначала срабатывает конструктор,
        // затем метод ConfigureServices() и в конце метод Configure(). Эти методы вызываются средой выполнения ASP.NET.

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // Добавляет сервисы для работы с API
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Метод Run представляет собой простейший способ для добавления компонентов middleware в конвейер.
            // Однако компоненты, определенные через метод Run,
            // не вызывают никакие другие компоненты и дальше обработку запроса не передают.
            // app.Run(async (context)=> await context.Response.WriteAsync("Fuck all!!!"));

            // Метод Use также добавляет компоненты middleware, которые также обрабатывают запрос,
            // но в нем может быть вызван следующий в конвейере запроса компонент middleware.
            // int x = 4;
            // int y = 5;
            // int z = 0;

            // app.Use(async (context, next) =>  {z = x * y;
            //     await next.Invoke();
            // });

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync($"Result {z}");
            //});

            // Тут добавляем компоненты в конвеер для обработки запроса. Запрос обработаывается по очереди
            // добавления компонентов в конвеер

            // При этом порядок определения компонентов играет большую роль.

            // компоненты middleware создаются один раз и живут в течение всего жизненного цикла приложения

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandler>();

            app.UseRouting();

            // испоьзуем тукен
            // app.UseToken("hello");

            // Метод Map (и методы расширения MapXXX()) применяется для сопоставления пути запроса с определенным делегатом,
            // который будет обрабатывать запрос по этому пути.
            app.Map("/index", Index);
            app.Map("/about", About);

            app.UseEndpoints(endpoints =>
            {
                // Это выражение указывает, что для всех запросах по маршруту "/" (то есть к корню веб-приложения) в
                // ответ будет отправляться строка "Hello World!".
                //endpoints.MapGet("/",
                //    async context =>
                //    {
                //        await context.Response.WriteAsync(
                //            $"Hello World! Application name: {_environment.ApplicationName}");
                //    });

                endpoints.MapGet("/fuck",
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            $"FUCKKKKKKK!");
                    });
            });

            // если мы хотим использовать имена своих статических файлов по умолчанию
            // var defaultSettings = new DefaultFilesOptions();
            // defaultSettings.DefaultFileNames.Clear();
            // defaultSettings.DefaultFileNames.Add("onelove.html");

            // app.UseDefaultFiles(defaultSettings); // теперь при заходе на базовый адрес, будет грузиться index.html

            // app.UseDirectoryBrowser(); // позволяет пользователям просматривать содержимое каталогов на сайте

            app.UseDirectoryBrowser(new DirectoryBrowserOptions() // разрешение использования браузера для файлов по пути "docs" с каталогом "disk"
            {
                FileProvider =
                    new PhysicalFileProvider(
                        Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\disk"))), 
                RequestPath = new PathString("/docs") 
            });

            app.UseDefaultFiles(new DefaultFilesOptions(new SharedOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
                RequestPath = new PathString("/pages")
            }));
         //   app.UseStaticFiles(); // enable static files
            app.UseStaticFiles(new StaticFileOptions() // разрешение использования пути "pages" с каталогом "html"
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\html")),
                RequestPath = new PathString("/pages")
            }); // enable static files
        }

        private static void Index(IApplicationBuilder app)
        {
            // вложенный map, т.е. обработка запроса "index/vasya"
            app.Map("/vasya", app => app.Run(async (context) => context.Response.WriteAsync("Fuck Vasya!!!!")));

            app.Run(async context => { await context.Response.WriteAsync("Index"); });
        }

        private static void About(IApplicationBuilder app)
        {
            app.Run(async context => { await context.Response.WriteAsync("About"); });
        }
    }
}
