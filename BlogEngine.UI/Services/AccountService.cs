using BlogEngine.Dto.Request;
using BlogEngine.Dto.Response;
using BlogEngine.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlogEngine.UI.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public AccountService(ILogger<AccountService> logger,
            IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<string> Authenticate(string userName, string password)
        {
            string result = string.Empty;
            try
            {
                var client = _clientFactory.CreateClient("BlogEngineAPI");

                CreateTokenRequestDto request = new CreateTokenRequestDto
                {
                    UserName = userName,
                    Password = password
                };

                var xdata = JsonSerializer.Serialize(request);

                HttpContent body = new StringContent(xdata, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Add("Accept", "application/json");
                
                var response = await client.PostAsync("/api/account/create-token", body);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<CreateTokenResponseDto>(content);

                    if (data.IsSucces && data.Token is not null)
                        result = data.Token.Value;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return result;
        }

        public async Task<Author> GetAuthor(string token)
        {
            Author result = null;
            try
            {
                var client = _clientFactory.CreateClient("BlogEngineAPI");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync("/api/account/get");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<AuthorResponseDto>(content);

                    if (data.IsSucces && data.Author is not null)
                        result = Helper.Common.Map(data.Author);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    _logger.LogError(content);
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
