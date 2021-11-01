using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace BlogEngine.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var _dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _dbContext.Database.EnsureCreated();

            if (_dbContext.Author.Any() == false)
            {
                _dbContext.Author.Add(new Domain.Entities.Author 
                { 
                    UserName = "puser",Password = "Tmp*0440", Name = "Public User", Role = 1 
                });

                _dbContext.Author.Add(new Domain.Entities.Author
                {
                    UserName = "wuser",
                    Password = "Tmp*0440",
                    Name = "Writer User",
                    Role = 2
                });

                _dbContext.Author.Add(new Domain.Entities.Author
                {
                    UserName = "euser",
                    Password = "Tmp*0440",
                    Name = "Editor User",
                    Role = 3
                });
            }

            _dbContext.SaveChanges();
        }
    }
}
