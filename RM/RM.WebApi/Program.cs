using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace RM.WebApi;

/// <summary>
/// Основной класс приложения ASP.NET Core Web API.
/// </summary>
/// <remarks>
/// Содержит точку входа в приложение (метод <see cref="Main"/>) и логику создания хоста.
/// <see cref="Program"/> связан с настройкой инфраструктуры приложения, которая меняется редко. 
/// </remarks>
public class Program
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="Program"/>.
    /// </summary>
    protected Program() { }

    /// <summary>
    /// Запускает веб‑приложение, создавая и запуская хост с настройками по умолчанию.
    /// </summary>
    /// <remarks>Точка входа в приложение ASP.NET Core.</remarks>
    /// <param name="args">Массив аргументов командной строки, переданных при запуске приложения.</param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()    
            .Run();     
    }

    /// <summary>
    /// Создаёт и настраивает построитель хоста (<see cref="IHostBuilder"/>) для веб‑приложения.
    /// </summary>
    /// <remarks>Использует стандартные настройки ASP.NET Core и подключает класс <see cref="Startup"/>
    /// для конфигурации сервисов и конвейера обработки запросов.</remarks>
    /// <param name="args">Аргументы командной строки.</param>
    /// <returns>Настроенный экземпляр <see cref="IHostBuilder"/>, готовый к созданию хоста.</returns>
    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                // Указывает использовать класс Startup для настройки приложения:
                // регистрации сервисов в DI‑контейнере и настройки конвейера middleware
                webBuilder.UseStartup<Startup>();
            });
}