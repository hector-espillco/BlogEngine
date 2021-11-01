using System.Linq;
using Models = BlogEngine.Shared.Models;

namespace BlogEngine.API.Helper
{
    public static class Common
    {
        public static Dto.Post Map(Models.Post post)
        {
            return new Dto.Post
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                AuthorName = post.AuthorName,
                PublishedDate = post.PublishedDate,
                Status = post.Status,
                Comments = post.Comments
                    .Select(c=> new Dto.Comment
                     {
                         Id = c.Id,
                         Content = c.Content,
                         AuthorId = c.AuthorId,
                         PostId = c.PostId
                     })
                     .ToList()
            };
        }

        public static Dto.Comment Map(Models.Comment comment)
        {
            return new Dto.Comment
            {
                Id = comment.Id,
                PostId = comment.PostId,
                AuthorId = comment.AuthorId,
                Content = comment.Content
            };
        }

        public static Dto.Author Map(Models.Author auhtor)
        {
            return new Dto.Author
            {
                Id = auhtor.Id,
                UserName = auhtor.UserName,
                Password = auhtor.Password,
                Name = auhtor.Name,
                Role = auhtor.Role
            };
        }
    }
}
