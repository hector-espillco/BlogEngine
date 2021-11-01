using BlogEngine.Shared.Models;
using BlogEngine.UI.Extensions;
using BlogEngine.UI.Services;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogEngine.UI.Pages
{
    public class LoginPage : BasePage
    {
        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public ISessionStorageService SessionStorageService { get; set; }

        protected Author Author { get; set; }

        protected string Message = string.Empty;

        protected override Task OnInitializedAsync()
        {
            Author = new Author();
            return base.OnInitializedAsync();
        }

        protected async Task<bool> ValidateUser()
        {
            bool result = false;
            try
            {
                Message = string.Empty;

                var token = await AccountService.Authenticate(Author.UserName, Author.Password);
                if (!string.IsNullOrEmpty(token))
                {
                    ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticated(token);
                    result = true;
                    NavigationManager.NavigateTo("Index");

                    await SessionStorageService.SetItemAsync("token", token);
                }
                else
                {
                    Message = "Username or Password invalid.";
                }
            }
            catch (Exception ex)
            {
                Message = "Username or Password invalid.";
                Logger.LogError(ex, ex.Message);
            }

            return await Task.FromResult(result);
        }
    }
}
