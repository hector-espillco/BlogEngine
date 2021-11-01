using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models = BlogEngine.Shared.Models;

namespace BlogEngine.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Models.Post>> GetPosts();
        Task<Models.Comment> AddComment(Models.Comment comment);
        Task<Models.Post> AddPost(Models.Post post);
        Task<Models.Post> EditPost(Models.Post post);
        Task<Models.Post> SubmitPost(Models.Post post);
        Task<IEnumerable<Models.Post>> GetPostsByApprove(int editorId);
        Task<Models.Post> ApprovePost(Models.Post post);
        Task<Models.Post> RejectedPost(Models.Post post);
        Task<IEnumerable<Models.Post>> GetPostsOfWriter(int authorId);
    }
}
