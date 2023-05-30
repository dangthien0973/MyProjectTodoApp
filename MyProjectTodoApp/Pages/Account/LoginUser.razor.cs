using Microsoft.AspNetCore.Components;
using MyProjectTodoApp.Services;

namespace MyProjectTodoApp.Pages.Account
{
    public partial class LoginUser
    {
        [Inject] ILogin Login { set; get; } 
        private Model.UserModel.LoginUser loginUser =new Model.UserModel.LoginUser();
        private bool isLoading { get; set; }
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        public async void OnValidSubmit()
        {
            isLoading = true;
            await Login.LoginUser(loginUser);
            isLoading = false;
        }
    }
}
