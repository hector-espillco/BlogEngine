using BlogEngine.Application.Interfaces;
using BlogEngine.Dto.Request;
using BlogEngine.Dto.Response;
using BlogEngine.Shared.Enums;
using BlogEngine.Shared.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger, IConfiguration configuration, IAccountService accountService)
        {
            _logger = logger;
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpPost("create-token")]
        public async Task<CreateTokenResponseDto> CreateToken(CreateTokenRequestDto request)
        {
            CreateTokenResponseDto response = new CreateTokenResponseDto();

            try
            {
                var account = await _accountService.GetAuthor(request.UserName,request.Password);

                if (account is not null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.UniqueName, account.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Role, ((Role)account.Role).GetValue())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Secret")));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var expiration = DateTime.Now.AddHours(2);

                    var token = new JwtSecurityToken
                    (
                        issuer: "https://demo.com",
                        audience: "https://demo.com",
                        claims: claims,
                        expires: expiration,
                        signingCredentials: creds
                    );

                    response.IsSucces = true;
                    response.Token = new Dto.Token
                    {
                        Value = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = expiration
                    };
                }
            }
            catch (Exception ex)
            {
                response.Errors = new HashSet<string> { "There was an internal error." };

                _logger.LogError(ex, ex.Message);
            }

            return response;
        }

        [HttpGet("get")]
        [Authorize(Roles = "Public,Writer,Editor")]
        public async Task<AuthorResponseDto> GetAuthor()
        {
            AuthorResponseDto response = new AuthorResponseDto();

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int authorId = 0;

                if (identity is not null)
                {
                    IEnumerable<Claim> claims = identity.Claims;

                    var claim = claims
                        .Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
                        .FirstOrDefault();

                    if (!int.TryParse(claim.Value, out authorId) ||
                        authorId == 0)
                        return response;
                }

                var author = await _accountService.GetAuthor(authorId);

                if (author is not null)
                {
                    response.IsSucces = true;
                    response.Author = Helper.Common.Map(author);
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
