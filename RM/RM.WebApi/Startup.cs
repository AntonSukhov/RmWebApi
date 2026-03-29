using System;
using System.IO;
using System.Linq;
using System.Reflection;
using IdentityWebApp.Api.Extensions;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using RM.WebApi.Extensions;
using RM.WebApi.Middleware;

namespace RM.WebApi;

/// <summary>
/// Компонент начальной конфигурации приложения ASP.NET Core.
/// </summary>
/// <remarks>
/// Отвечает за настройку сервисов (DI‑контейнер) и конвейера обработки HTTP‑запросов.
/// </remarks>
internal class Startup: Infrastructure.AspNetCore.StartupBase
{
    /// <summary>
    /// Инициализирует экземпляр <see cref="Startup"/>.
    /// </summary>
    /// <param name="configuration">Объект конфигурации приложения.</param>
    /// <param name="environment">Объект окружения приложения.</param>
    public Startup(IConfiguration configuration, IWebHostEnvironment environment):
        base(configuration, environment) {}

    /// <summary>
    /// Выполняет регистрацию сервисов, используемых в приложении.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    public override void ConfigureServices(IServiceCollection services)
    {      
        base.ConfigureServices(services);

        services.RegisterDbContexts(Configuration);
        services.RegisterSettings(Configuration);
        services.RegisterRepositories(); 
        services.AddIdentityWebAppAuthentication();        
        services.RegisterServices();
        services.RegisterValidators();
        services.RegisterMappingProfiles();
        
        services.AddControllers();
        services.AddRazorPages(); 
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "RM.WebApi", Version = "v1" });

            // Укажите пути к XML-файлам с комментариями
            var xmlFiles = new[]
            {
                $"{Assembly.GetExecutingAssembly().GetName().Name}.xml", // XML текущего проекта
                "RM.BLL.Abstractions.xml",                               // XML первого связанного проекта
            };

            foreach (var xmlFile in xmlFiles.Select(file => Path.Combine(AppContext.BaseDirectory, file))
                                            .Where(xmlFile => File.Exists(xmlFile)))
            {
                c.IncludeXmlComments(xmlFile);
            }
        });
    }

    /// <summary>
    /// Выполняет настройку конвейера HTTP-запросов. Вызывается средой выполнения.
    /// </summary>
    /// <param name="app">Веб-приложение.</param>
    public override void Configure(WebApplication app)
    {       
        base.Configure(app);

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RM.WebApi v1"));
        }

        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllers();
        app.MapRazorPages();
    }
}
