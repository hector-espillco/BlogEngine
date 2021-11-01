using BlogEngine.Application.Interfaces;
using BlogEngine.Dto.Request;
using BlogEngine.Dto.Response;
using BlogEngine.Shared.Helper;
using BlogEngine.Shared.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models = BlogEngine.Shared.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace BlogEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }
        
        [HttpGet("list")]
        [Authorize(Roles = "Public,Writer,Editor")]
        public async Task<PostsResponseDto> GetPosts()
        {
            PostsResponseDto response = new PostsResponseDto();

            try
            {
                var posts = await _postService.GetPosts();

                if (posts.Any())
                {
                    response.IsSucces = true;
                    response.Posts = posts.Select(p => Helper.Common.Map(p));
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpPost("comment/add")]
        [Authorize(Roles = "Public,Writer,Editor")]
        public async Task<CommentResponseDto> AddComment(CommentRequestDto request)
        {
            CommentResponseDto response = new CommentResponseDto();

            try
            {
                var model = new Models.Comment
                {
                    PostId = request.PostId,
                    AuthorId = request.AuthorId,
                    Content = request.Content
                };

                var comment = await _postService.AddComment(model);

                if (comment is not null)
                {
                    response.IsSucces = true;
                    response.Comment = Helper.Common.Map(comment);
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpGet("list/writer/{id}")]
        [Authorize(Roles = "Writer,Editor")]
        public async Task<PostsResponseDto> GetPostsOfWriter(int id)
        {
            PostsResponseDto response = new PostsResponseDto();

            try
            {
                var posts = await _postService.GetPostsOfWriter(id);

                if (posts.Any())
                {
                    response.IsSucces = true;
                    response.Posts = posts.Select(p => Helper.Common.Map(p));
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpPost("add")]
        [Authorize(Roles = "Writer,Editor")]
        public async Task<PostResponseDto> AddPost(CreatePostRequestDto request)
        {
            PostResponseDto response = new PostResponseDto();

            try
            {
                //var identity = HttpContext.User.Identity as ClaimsIdentity;
                //int authorId = 0;

                //if (identity is not null)
                //{
                //    IEnumerable<Claim> claims = identity.Claims;
                    
                //    if (!int.TryParse(identity.FindFirst(JwtRegisteredClaimNames.UniqueName).Value,out authorId) ||
                //        authorId == 0 || authorId != request.AuthorId)
                //        return response;
                //}

                var model = new Models.Post
                {
                    AuthorId = request.AuthorId,
                    Title = request.Title,
                    Content = request.Content
                };

                var post = await _postService.AddPost(model);

                if (post is not null)
                {
                    response.IsSucces = true;
                    response.Post = Helper.Common.Map(post);
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpPost("edit")]
        [Authorize(Roles = "Writer,Editor")]
        public async Task<PostResponseDto> EditPost(EditPostRequestDto request)
        {
            PostResponseDto response = new PostResponseDto();

            try
            {
                var model = new Models.Post
                {
                    Id = request.PostId,
                    AuthorId = request.AuthorId,
                    Title = request.Title,
                    Content = request.Content
                };

                var post = await _postService.EditPost(model);

                if (post is not null)
                {
                    response.IsSucces = true;
                    response.Post = Helper.Common.Map(post);
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpPost("submit")]
        [Authorize(Roles = "Writer,Editor")]
        public async Task<PostResponseDto> SubmitPost(SubmitPostRequestDto request)
        {
            PostResponseDto response = new PostResponseDto();

            try
            {
                var model = new Models.Post
                {
                    Id = request.PostId,
                    AuthorId = request.AuthorId
                };

                var post = await _postService.SubmitPost(model);

                if (post is not null)
                {
                    response.IsSucces = true;
                    response.Post = Helper.Common.Map(post);
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpGet("list/editor/{id}")]
        [Authorize(Roles = "Editor")]
        public async Task<PostsResponseDto> GetPostsByApprove(int id)
        {
            PostsResponseDto response = new PostsResponseDto();

            try
            {
                var posts = await _postService.GetPostsByApprove(id);

                if (posts.Any())
                {
                    response.IsSucces = true;
                    response.Posts = posts.Select(p => Helper.Common.Map(p));
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpPost("approve")]
        [Authorize(Roles = "Editor")]
        public async Task<PostResponseDto> ApprovePost(ApprovePostRequestDto request)
        {
            PostResponseDto response = new PostResponseDto();

            try
            {
                var model = new Models.Post
                {
                    Id = request.PostId,
                    AuthorId = request.AuthorId
                };

                var post = await _postService.ApprovePost(model);

                if (post is not null)
                {
                    response.IsSucces = true;
                    response.Post = Helper.Common.Map(post);
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpPost("reject")]
        [Authorize(Roles = "Editor")]
        public async Task<PostResponseDto> RejectPost(RejectPostRequestDto request)
        {
            PostResponseDto response = new PostResponseDto();

            try
            {
                var model = new Models.Post
                {
                    Id = request.PostId,
                    AuthorId = request.AuthorId,
                    Content = request.Content
                };

                var post = await _postService.RejectedPost(model);

                if (post is not null)
                {
                    response.IsSucces = true;
                    response.Post = Helper.Common.Map(post);
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

    }
}
