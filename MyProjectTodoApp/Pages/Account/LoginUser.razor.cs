using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using MyProjectTodoApp.Services;
using MyProjectTodoApp.Shared;

namespace MyProjectTodoApp.Pages.Account
{
    public partial class LoginUser
    {
        [Inject] ILogin Login { set; get; }
        public Error? Error { get; set; }
        [Inject] IToastService ToastService { set; get; }
        [Inject] private NavigationManager NavigationManager { set; get; }

        private Model.UserModel.LoginUser loginUser =new Model.UserModel.LoginUser();
        private bool isLoading { get; set; }
        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        public async void OnValidSubmit()
        {
            try
            {


                isLoading = true;
                await Login.LoginUser(loginUser);
                isLoading = false;
            }
            catch(Exception ex)
            {
                isLoading = false;
                Error?.ProcessError(ex);
            }
        }
    }
}
