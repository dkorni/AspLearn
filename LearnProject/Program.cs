using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LearnProject
{
    /// <summary>
    /// Запускает веб-хост в рамках которого запускается приложение
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // После этого приложение запущено, и веб-сервер начинает прослушивать все входящие HTTP-запросы.
            CreateHostBuilder(args).
                Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args) // IHostBuilder
                                            // Устанавливает корневой каталог. Где производиться поиск различного содержимого, например, представлений
                                            // Устанавливает конфигурацию хоста. 
                                            // Устанавливает конфигурацию приложения.
                                            // Добавляет провайдеры логирования
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // Загружает конфигурацию из переменных среды с префиксом "ASPNETCORE_"
                    // Запускает и настраивает веб-сервер Kestrel, в рамках которого будет разворачиваться приложение
                    // Добавляет компонент Host Filtering, который позволяет настраивать адреса для веб-сервера Kestrel
                    // ... 

                    // Этим вызовом устанавливается стартовый класс приложения - класс Startup, с которого и будет начинаться обработка входящих запросов.
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseWebRoot("Static"); // устанавливаем директорию с статическими файлами вручную
                });
    }
}
