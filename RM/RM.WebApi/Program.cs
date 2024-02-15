using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace RM.WebApi;

public class Program
{
    #region Конструкторы

    protected Program() { }

    #endregion

    #region Методы

    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    #endregion
}