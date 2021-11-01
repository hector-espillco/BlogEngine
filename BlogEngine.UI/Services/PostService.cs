using BlogEngine.Dto.Response;
using BlogEngine.Shared.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BlogEngine.UI.Helper;
using System.Net.Http.Headers;

namespace BlogEngine.UI.Services
{
    public class PostService : IPostService
    {
        private readonly ILogger<PostService> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public PostService(ILogger<PostService> logger,IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<List<Post>> GetPosts(string token)
        {
            List<Post> result = null;
            try
            {
                var client = _clientFactory.CreateClient("BlogEngineAPI");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync("/api/post/list");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<PostsResponseDto>(content);

                    if (data.IsSucces && data.Posts is not null && data.Posts.Any())
                        result = data.Posts.Select(p => Common.Map(p)).ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }
    }
}
