using BlogEngine.Application.Interfaces;
using BlogEngine.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IServiceScopeFactory<IApplicationDbContext> _contextFactory;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IServiceScopeFactory<IApplicationDbContext> contextFactory, ILogger<AccountService> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<Author> GetAuthor(string userName, string password)
        {
            Author result = null;

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var entity = await context.Author
                        .Where(a => a.UserName == userName && a.Password == password)
                        .FirstOrDefaultAsync();

                    if (entity is not null)
                    {
                        result = new Author
                        {
                            Id = entity.Id,
                            UserName = entity.UserName,
                            Password = entity.Password,
                            Name = entity.Name,
                            Role = entity.Role
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<Author> GetAuthor(int id)
        {
            Author result = null;

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var entity = await context.Author
                        .FindAsync(id);

                    if (entity is not null)
                    {
                        result = new Author
                        {
                            Id = entity.Id,
                            UserName = entity.UserName,
                            Password = entity.Password,
                            Name = entity.Name,
                            Role = entity.Role
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }
    }
}
