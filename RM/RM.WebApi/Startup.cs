using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using RM.WebApi.Extensions;
using RM.WebApi.Middleware;

namespace RM.WebApi
{
    /// <summary>
    /// Класс начальной конфигурации приложения ASP.NET Core.
    /// </summary>
    /// <remarks>
    /// Отвечает за настройку сервисов (DI‑контейнер) и конвейера обработки HTTP‑запросов.
    /// </remarks>
    public class Startup
    {
        /// <summary>
        /// Инициализирует экземпляр <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration">Настройки приложения.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Получает настройки приложения.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Выполняет регистрацию сервисов, используемых в приложении.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.RegisterDbContexts(Configuration);
            services.RegisterSettings(Configuration);
            services.RegisterRepositories();         
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
        /// <param name="app">Создатель компонентов конвейера HTTP-запросов.</param>
        /// <param name="env">Информация о среде веб-хостинга, в которой работает приложение.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {        
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();

                // Запускает веб‑интерфейс Swagger UI для интерактивного тестирования API.
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RM.WebApi v1"));
            }

            // Глобальный обработчик исключений
            app.UseMiddleware<ErrorHandlingMiddleware>();

            // Перенаправляет HTTP‑запросы на HTTPS.
            app.UseHttpsRedirection();

            //Для обслуживания CSS/JS
            app.UseStaticFiles();

            // Настраивает маршрутизацию — определяет, какой код будет вызван для каждого URL
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
