using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RM.WebApi.Extensions;
using RM.WebApi.Middleware;

namespace RM.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterDbContexts(Configuration);
            services.RegisterRepositories();         
            services.RegisterServices();
            services.RegisterValidators();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RM.WebApi", Version = "v1" });
            });
        }

        /// <summary>
        /// Настройка конвейера HTTP-запросов. Вызывается средой выполнения.
        /// </summary>
        /// <param name="app">Создаёт компоненты конвейера HTTP-запросов.</param>
        /// <param name="env">Предоставляет информацию о среде веб-хостинга, в которой работает приложение.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {        

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RM.WebApi v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
