using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Win32;
using Model.UserModel;
using MyProjectTodoApp.Pages.Account;
using System.Reflection;

namespace MyProjectTodoApp.Services
{
    public class Login : ILogin
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        private string _userKey = "user";
        [Inject] IToastService _toastService { set; get; }

        public LoginReponse User { get; private set; }
        public Login(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService, IToastService toastService
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _toastService = toastService;

        }
        public async Task LoginUser(Model.UserModel.LoginUser loginUser)
        {
            User = await _httpService.Post<LoginReponse>("/api/Login", loginUser);
            await _localStorageService.SetItem(_userKey, User);
            _toastService.ShowSuccess($"Login success! Welcome {User.FullName}" );
            _navigationManager.NavigateTo("/todoList");
        }

        public async Task Logout()
        {
            User = null;
            await _localStorageService.RemoveItem(_userKey);
        }

        public  async Task Register(Model.UserModel.RegisterUser registerUser)
        {
            var result = await _httpService.Post<Model.UserModel.RegisterUser>("/api/Login/Register", registerUser);
        }
    }
}
