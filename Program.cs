using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MessageService
{
    /// <summary>
    /// Класс с основной программой.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        /// <param name="args"> Массив строк </param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Создание хоста.
        /// </summary>
        /// <param name="args"> Массив строк </param>
        /// <returns> Постройщик хоста </returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
