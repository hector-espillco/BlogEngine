using BlogEngine.Application;
using BlogEngine.Application.Interfaces;
using BlogEngine.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.API.Extensions
{
    public static class Application
    {
        public static IServiceCollection AddCustomApplication(this IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddSingleton(typeof(IServiceScopeFactory<>), typeof(ServiceScopeFactory<>));

            return services;
        }
    }
}
