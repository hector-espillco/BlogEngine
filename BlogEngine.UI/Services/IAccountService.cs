using BlogEngine.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Shared.Models;

namespace BlogEngine.UI.Services
{
    public interface IAccountService
    {
        Task<string> Authenticate(string userName,string password);
        Task<Author> GetAuthor(string token);
    }
}
