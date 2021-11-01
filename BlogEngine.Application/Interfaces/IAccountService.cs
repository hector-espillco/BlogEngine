using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogEngine.Shared.Models;

namespace BlogEngine.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Author> GetAuthor(string userName, string passord);
        Task<Author> GetAuthor(int id);
    }
}
