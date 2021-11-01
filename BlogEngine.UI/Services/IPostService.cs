using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = BlogEngine.Shared.Models;

namespace BlogEngine.UI.Services
{
    public interface IPostService
    {
        Task<List<Models.Post>> GetPosts(string token);
    }
}
