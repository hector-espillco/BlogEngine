using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Models = BlogEngine.Shared.Models;
using BlogEngine.UI.Services;

namespace BlogEngine.UI.Pages
{
    public class IndexPage: BasePage
    {
        [Inject]
        public ISessionStorageService SessionStorageService { get; set; }

        [Inject]
        public IPostService PostService { get; set; }

        [Inject]
        public IAccountService AccountService { get; set; }

        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationState { get; set; }

        protected List<Models.Post> Posts { get; set; }
        protected Models.Author Author { get; set; } = new Models.Author();

        protected async override Task OnInitializedAsync()
        {
            var token = await GetToken();
            Author = await AccountService.GetAuthor(token);
            Posts = await PostService.GetPosts(token);
        }

        protected void SeeComments(Models.Post post)
        {
            post.SeeComments = true;
        }

        protected void HideComments(Models.Post post)
        {
            post.SeeComments = false;
        }

        private async Task<string> GetToken()
        {
            var user = (await AuthenticationState).User;
            var claim = user.Claims.Where(c => c.Type == "token")
                .FirstOrDefault();

            if (claim is not null)
                return claim.Value;
            else
                return string.Empty;
        }

        
    }
}
