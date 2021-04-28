using GreenFlux.DataAccess.EF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Microsoft.DependencyInjection;

namespace GreenFlux.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();

            using (var scope = hostBuilder.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
              
                try
                {
                    var context = serviceProvider.GetRequiredService<DatabaseContext>();
                    DatabaseContext.SeedExamples(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during seeding the database.");
                    throw ex;
                }
            }

            hostBuilder.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseUnityServiceProvider()   // Add Unity as default Service Provider
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
