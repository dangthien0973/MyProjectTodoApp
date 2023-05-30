using Microsoft.AspNetCore.Components;
using Model.UserModel;
using System.Reflection;

namespace MyProjectTodoApp.Services
{
    public class Login : ILogin
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "user";

        public LoginReponse User { get; private set; }
        public Login(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
        }
        public async Task LoginUser(LoginUser loginUser)
        {
            User = await _httpService.Post<LoginReponse>("/users/authenticate", loginUser);
            await _localStorageService.SetItem(_userKey, User);
        }
    }
}
