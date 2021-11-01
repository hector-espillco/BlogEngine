using BlogEngine.Application.Interfaces;
using BlogEngine.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.IO;
using System.Text;

namespace BlogEngine.API.Extensions
{
    public static class Infrastructure
    {
        public static IServiceCollection AddCustomInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //database

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Connection"));
            });

            services.AddScoped<IApplicationDbContext>(s => s.GetService<ApplicationDbContext>());

            //logging
            Log.Logger = new LoggerConfiguration()
                        .WriteTo.File(Path.Combine(AppContext.BaseDirectory, $"Logs/BlogEngine-{DateTime.Now:yyyyMMdd}.txt"))
                        .CreateLogger();

            services.AddLogging(configure =>
            {
                configure.AddSerilog();
            });

            //authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "https://demo.com",
                    ValidIssuer = "https://demo.com",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Secret")))
                };
            });

            return services;
        }
    }
}
