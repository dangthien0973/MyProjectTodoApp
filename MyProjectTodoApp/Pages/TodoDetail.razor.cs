using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Model;
using MyProjectTodoApp.Services;
using System.Security.Cryptography;

namespace MyProjectTodoApp.Pages
{
    public partial class TodoDetail
    {
        [Inject] private ITaskApiClient TaskApiClient { set; get; }
        [Inject] private NavigationManager NavigationManager { set; get; }
        [Inject] IToastService ToastService { set; get; }

        [Parameter]
        public string Id { set; get; }
        private TodoDto todoDto { set; get; }
        private TodoUpdateRequest todoUpdateRequest { set; get; }


        protected override async Task OnInitializedAsync()
        {
            todoDto = await TaskApiClient.GetTaskDetail(Id);
            todoUpdateRequest = new TodoUpdateRequest();
            todoUpdateRequest.Name = todoDto.Name;  
            todoUpdateRequest.Priority = todoDto.Priority;
        }
        public async void UpdateTask()
        {

            try
            {


                var result = await TaskApiClient.UpdateTask(new Guid(Id), todoUpdateRequest);
                if (result)
                {
                    ToastService.ShowSuccess("This Todo was updated sucess !");
                    NavigationManager.NavigateTo("/todoList");
                }
                else
                {
                    ToastService.ShowError("It is some problems while updating data , Please try later ! Sorry about that !");
                }
            }
            catch(Exception e)
            {
                ToastService.ShowError("It is some problems while update data , Please contact us  !");
            }
             

        }
    }
}
