using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using MessageService.Repositories;
using System.IO;

namespace MessageService
{
    /// <summary>
    /// Класс стартапа.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Конструктор стартапа.
        /// </summary>
        /// <param name="configuration"> Настройка конфигурации </param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Конфигурация.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Настройка служб.
        /// </summary>
        /// <param name="services"> Обслуживание </param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IMessageRepository, MessageRepository>();
             
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MessageService", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "MyMessageService.xml");
                c.IncludeXmlComments(filePath);
            });

        }

        /// <summary>
        /// Конфигурирует приложение.
        /// </summary>
        /// <param name="app"> Разработчик приложения </param>
        /// <param name="env"> Среда веб-хостинга </param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MessageService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
