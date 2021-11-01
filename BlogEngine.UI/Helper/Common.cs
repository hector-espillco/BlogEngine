using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = BlogEngine.Shared.Models;
using Dto = BlogEngine.Dto;

namespace BlogEngine.UI.Helper
{
    public static class Common
    {
        public static Models.Post Map(Dto.Post post)
        {
            return new Models.Post
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                AuthorId = post.AuthorId,
                AuthorName = post.AuthorName,
                PublishedDate = post.PublishedDate,
                Status = post.Status,
                Comments = post.Comments
                    .Select(c=> new Models.Comment 
                    { 
                        Id = c.Id,
                         AuthorId = c.AuthorId,
                          Content = c.Content,
                           PostId = c.PostId
                    })
                    .ToList()
            };
        }

        public static Models.Author Map(Dto.Author author)
        {
            return new Models.Author
            {
                Id = author.Id,
                UserName = author.UserName,
                Password = author.Password,
                Name =author.Name,
                Role = author.Role
            };
        }
    }
}
