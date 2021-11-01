using BlogEngine.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.UI.Extensions
{
    public static class Application
    {
        public static IServiceCollection AddCustomApplication(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPostService, PostService>();

            return services;
        }
    }
}
