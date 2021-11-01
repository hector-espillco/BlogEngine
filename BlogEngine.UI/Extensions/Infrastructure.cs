using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.UI.Extensions
{
    public static class Infrastructure
    {
        public static IServiceCollection AddCustomInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //loging
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.File(Path.Combine(AppContext.BaseDirectory, $"Logs/BlogEngineUI-{DateTime.Now:yyyyMMdd}.txt"))
                        .CreateLogger();

            services.AddLogging(configure =>
            {
                configure.AddSerilog();
            });

            //API
            services.AddHttpClient("BlogEngineAPI", config =>
            {
                config.BaseAddress = new Uri(configuration["BlogEngigeAPIUrl"]);
            });

            //Session
            services.AddBlazoredSessionStorage();

            return services;
        }
    }
}
