using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Model;
using Model.SeekWork;
using MyProjectTodoApp.Component;
using MyProjectTodoApp.Services;
using MyProjectTodoApp.Shared;
using System.Reflection.Metadata;

namespace MyProjectTodoApp.Pages
{
    public partial class TodoList
    {
        [CascadingParameter]
        public Error? Error { get; set; }
        [Inject] IToastService ToastService { set; get; }
        [Inject] private ITaskApiClient TaskApiClient { set; get; } 
        [Inject] private IUserApiClient UserApiClient { set; get; } 
        private List<TodoDto> TodoDto { set; get; }
        private List<AssigneeDto> ListAssigneeDto { set; get; }

        private TaskListSearch TaskListSearch = new TaskListSearch();
        protected Confirmtion DeleteConfirmation { set; get; }

        private PagedList<TodoDto>  PagedListTodo { set; get; }
        private Guid DeleteId { set; get; }
        public MetaData MetaData { get; set; } = new MetaData();
        protected override async Task OnInitializedAsync()
        {
            TaskListSearch taskListSearch = new TaskListSearch();
            ListAssigneeDto = await UserApiClient.GetAssignees();
            await SearchTask();
        }
        public async Task SearchTask()
        {
            PagedListTodo = await TaskApiClient.GetTaskList1(TaskListSearch);
            if(PagedListTodo != null)
            {
                TodoDto = PagedListTodo.Items;
                MetaData = PagedListTodo.MetaData;
            }
            else
            {
                ToastService.ShowError($"Some thing is wrong when searching data . Please try again");
            }
        }
        public async Task Delete(Guid id)
        {
            try
            {
                DeleteId = id;
                DeleteConfirmation.Show();
            }
            catch(Exception ex)
            {
                Error?.ProcessError(ex);
            }
        }
        public async Task OnConfirmDeleteTask(bool deleteConfirmed)
        {
            try
            {


                if (deleteConfirmed)
                {
                    TodoDto todoDto = new TodoDto();
                    todoDto = await TaskApiClient.DeleteTask(DeleteId.ToString());
                    if (todoDto != null)
                    {
                        ToastService.ShowSuccess($"{todoDto.Name} has been deleted successfully.");
                        await SearchTask();
                    }
                    else
                    {
                        ToastService.ShowError($"Some thing is wrong when delete data . Please try again");
                    }
                    await SearchTask();
                }
            }
            catch (Exception ex)
            {
                Error?.ProcessError(ex);
            }
        
        }
        private async Task SelectedPage(int page)
        {
            TaskListSearch.PageNumber = page;
            await SearchTask();
        }

    }
}
