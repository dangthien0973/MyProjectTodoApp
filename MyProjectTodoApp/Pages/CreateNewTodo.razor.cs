using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Model;
using MyProjectTodoApp.Services;
using System.Net.WebSockets;

namespace MyProjectTodoApp.Pages
{
    public partial class CreateNewTodo
    {
        [Inject] private ITaskApiClient TaskApiClient { set; get; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject]   IToastService ToastService { set; get; }


        private TodoCreateRequest TodoCreateRequest =new TodoCreateRequest();

        protected override Task OnInitializedAsync() { 
        
            return base.OnInitializedAsync();

        }
        public async void SubmitTask()
        {
     
            var result  = await TaskApiClient.CreateTask(TodoCreateRequest);
            if (result)
            {
                ToastService.ShowSuccess($"{TodoCreateRequest.Name} has been created successfully.");
                NavigationManager.NavigateTo("/todoList");
            }
            else
            {
                ToastService.ShowError($"An error occurred in progress. Please contact to administrator.");
            }
        }

    }
}
