using BlogEngine.Application.Interfaces;
using BlogEngine.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities = BlogEngine.Domain.Entities;
using BlogEngine.Shared.Enums;

namespace BlogEngine.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IServiceScopeFactory<IApplicationDbContext> _contextFactory;
        private readonly ILogger<PostService> _logger;

        public PostService(IServiceScopeFactory<IApplicationDbContext> contextFactory, ILogger<PostService> logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            List<Post> result = new List<Post>();

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var items = await (from post in context.Post
                                       join authorPost in context.Author
                                       on post.AuthorId equals authorPost.Id
                                       join comment in context.Comment
                                       on post.Id equals comment.PostId into rows
                                       from row in rows.Where(c => c.Available == (int)Available.All)
                                                       .DefaultIfEmpty()
                                       where post.PublishedDate.HasValue
                                       select new
                                       {
                                           post.Id,
                                           post.Title,
                                           post.Content,
                                           post.PublishedDate,
                                           post.AuthorId,
                                           post.Status,
                                           AuthorPost = authorPost.Name,
                                           ContentComment = row.Content,
                                           IdComment = row == null ? 0 : row.Id
                                       }).ToListAsync();

                    var posts = items.GroupBy(p => p.Id)
                        .Select(p=> p.Key)
                        .ToList();

                    foreach (var post in posts)
                    {
                        var newPost = items.Where(p => p.Id == post)
                            .Select(p=> new { p.Title,p.Content,p.PublishedDate,p.Status,p.AuthorId,p.AuthorPost })
                            .First();

                        result.Add(new Post
                        {
                            Id = post,
                            Title = newPost.Title,
                            Content = newPost.Content,
                            PublishedDate = newPost.PublishedDate,
                            AuthorId = newPost.AuthorId,
                            Status = newPost.Status,
                            AuthorName = newPost.AuthorPost,
                            Comments = items
                                .Where(p => p.Id == post && p.IdComment != 0)
                                .Select(c => new Comment { Content = c.ContentComment })
                                .ToList()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            Comment result = null;

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var post = await context.Post.FindAsync(comment.PostId);

                    if (post is null)
                    {
                        _logger.LogWarning("Post not found.");

                        return result;
                    }

                    if (!post.PublishedDate.HasValue)
                    {
                        _logger.LogWarning("Post is not published.");

                        return result;
                    }

                    var entiy = new Entities.Comment
                    {
                        PostId = comment.PostId,
                        AuthorId = comment.AuthorId,
                        Content = comment.Content,
                        Available = (int)Available.All
                    };

                    await context.Comment.AddAsync(entiy);
                    await context.SaveChangesAsync();

                    result = new Comment
                    {
                        Id = entiy.Id,
                        PostId = entiy.PostId,
                        AuthorId = entiy.AuthorId,
                        Content = entiy.Content,

                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<IEnumerable<Post>> GetPostsOfWriter(int authorId)
        {
            List<Post> result = new List<Post>();

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var items = await context.Post
                        .Join(context.Author, p => p.AuthorId, a => a.Id,
                        (p, a) => new { p.Id, p.AuthorId, p.Title, p.Content, p.PublishedDate, p.Status, a.Role })
                        .Where(p => p.AuthorId == authorId)
                        .ToListAsync();

                    if (items is not null && items.Any())
                    {
                        foreach (var item in items)
                        {
                            result.Add(new Post
                            {
                                Id = item.Id,
                                AuthorId = item.AuthorId,
                                Title = item.Title,
                                Content = item.Content,
                                PublishedDate = item.PublishedDate,
                                Status = item.Status
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<Post> AddPost(Post post)
        {
            Post result = null;

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var entiy = new Entities.Post
                    {
                        AuthorId = post.AuthorId,
                        Title = post.Title,
                        Content = post.Content,
                        Status = (int)Status.Created
                    };

                    await context.Post.AddAsync(entiy);
                    await context.SaveChangesAsync();

                    result = new Post
                    {
                        Id = entiy.Id,
                        AuthorId = entiy.AuthorId,
                        Title = entiy.Title,
                        Content = entiy.Content,
                        Status = entiy.Status
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<Post> EditPost(Post post)
        {
            Post result = null;

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var entity = await context.Post.FindAsync(post.Id);

                    if (entity is null)
                    {
                        _logger.LogWarning("Post not found.");

                        return result;
                    }

                    if (entity.AuthorId != post.AuthorId)
                    {
                        _logger.LogWarning("Author has not created this post.");

                        return result;
                    }

                    if (!(entity.Status == (int)Status.Created ||
                        entity.Status == (int)Status.Rejected))
                    {
                        _logger.LogWarning("The status of post must be created or rejected.");

                        return result;
                    }

                    entity.Title = post.Title;
                    entity.Content = post.Content;

                    context.Post.Update(entity);

                    await context.SaveChangesAsync();

                    result = new Post
                    {
                        Id = entity.Id,
                        AuthorId = entity.AuthorId,
                        Title = entity.Title,
                        Content = entity.Content,
                        Status = entity.Status
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<Post> SubmitPost(Post post)
        {
            Post result = null;

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var entity = await context.Post.FindAsync(post.Id);

                    if (entity is null)
                    {
                        _logger.LogWarning("Post not found.");

                        return result;
                    }

                    if (entity.AuthorId != post.AuthorId)
                    {
                        _logger.LogWarning("Author has not created this post.");

                        return result;
                    }

                    if (!(entity.Status == (int)Status.Created || 
                        entity.Status == (int)Status.Rejected))
                    {
                        _logger.LogWarning("The status of post must be created or rejected.");

                        return result;
                    }

                    entity.Status = (int)Status.Submitted;

                    context.Post.Update(entity);

                    await context.SaveChangesAsync();

                    result = new Post
                    {
                        Id = entity.Id,
                        AuthorId = entity.AuthorId,
                        Title = entity.Title,
                        Content = entity.Content,
                        Status = entity.Status
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<IEnumerable<Post>> GetPostsByApprove(int editorId)
        {
            List<Post> result = new List<Post>();

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var editor = await context.Author.FindAsync(editorId);

                    if (editor is null || editor.Role != (int)Role.Editor)
                    {
                        _logger.LogWarning("Editor not found.");

                        return result;
                    }

                    var items = await context.Post
                        .Where(p => p.Status == (int)Status.Submitted)
                        .ToListAsync();

                    if (items is not null && items.Any())
                    {
                        foreach (var item in items)
                        {
                            result.Add(new Post
                            {
                                Id = item.Id,
                                AuthorId = item.AuthorId,
                                Title = item.Title,
                                Content = item.Content,
                                PublishedDate = item.PublishedDate,
                                Status = item.Status
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<Post> ApprovePost(Post post)
        {
            Post result = null;

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var entity = await context.Post.FindAsync(post.Id);

                    if (entity is null)
                    {
                        _logger.LogWarning("Post not found.");

                        return result;
                    }

                    if (entity.Status != (int)Status.Submitted)
                    {
                        _logger.LogWarning("The status of post must be submitted");

                        return result;
                    }

                    entity.Status = (int)Status.Approved;
                    entity.PublishedDate = DateTime.Now;

                    context.Post.Update(entity);

                    await context.SaveChangesAsync();

                    result = new Post
                    {
                        Id = entity.Id,
                        AuthorId = entity.AuthorId,
                        Title = entity.Title,
                        Content = entity.Content,
                        PublishedDate = entity.PublishedDate,
                        Status = entity.Status
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return result;
        }

        public async Task<Post> RejectedPost(Post post)
        {
            Post result = null;

            try
            {
                using (var scope = _contextFactory.CreateScope())
                {
                    var context = scope.GetRequiredService();

                    var entity = await context.Post.FindAsync(post.Id);

                    if (entity is null)
                    {
                        _logger.LogWarning("Post not found.");

                        return result;
                    }

                    if (entity.Status != (int)Status.Submitted)
                    {
                        _logger.LogWarning("The status of post must be submitted");

                        return result;
                    }

                    entity.Status = (int)Status.Rejected;

                    if (!string.IsNullOrEmpty(post.Content))
                    {
                        var comment = new Entities.Comment
                        {
                            AuthorId = post.AuthorId,
                            Content = post.Content,
                            PostId = entity.Id,
                            Available = (int)Available.OnlyWriter
                        };

                        await context.Comment.AddAsync(comment);
                    }

                    context.Post.Update(entity);

                    await context.SaveChangesAsync();

                    result = new Post
                    {
                        Id = entity.Id,
                        AuthorId = entity.AuthorId,
                        Title = entity.Title,
                        Content = entity.Content,
                        PublishedDate = entity.PublishedDate,
                        Status = entity.Status
                    };
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
