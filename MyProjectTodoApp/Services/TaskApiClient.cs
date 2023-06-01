using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Model;
using Model.SeekWork;
using Model.UserModel;
using MyProjectTodoApp.Pages.Account;
using System;
using System.Net.Http.Json;

namespace MyProjectTodoApp.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        // varibale
        private readonly ILogin _login;
        private readonly IHttpService _httpService;
        public HttpClient _httpClient;
        [Inject] IToastService _toastService { set; get; }
  
        public LoginReponse User { get; private set; }
        private string _userKey = "user";


        public TaskApiClient(HttpClient httpClient,ILogin login, IHttpService httpService)
        {
            _httpClient = httpClient;
            _login = login;
            _httpService = httpService;
        }

        public async Task<bool> CreateTask(TodoCreateRequest request)
        {    
                var result = await _httpClient.PostAsJsonAsync("/api/Todos", request);
                return result.IsSuccessStatusCode;
        }

        public async Task<TodoDto> DeleteTask(string id)
        {
            string url = $"/api/Todos/{id}";
            var result = await _httpClient.DeleteFromJsonAsync<TodoDto>(url);
            return result;
        }

        public async Task<PagedList<TodoDto>> GetMyTasks(TaskListSearch taskListSearch)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                queryStringParam.Add("name", taskListSearch.Name);
            if (taskListSearch.AssigneeId.HasValue)
                queryStringParam.Add("assigneeId", taskListSearch.AssigneeId.ToString());
            if (taskListSearch.Priority.HasValue)
                queryStringParam.Add("priority", taskListSearch.Priority.ToString());

            string url = QueryHelpers.AddQueryString("/api/tasks/me", queryStringParam);
            var result = await _httpClient.GetFromJsonAsync<PagedList<TodoDto>>(url);
            return result;
        }

        public async Task<TodoDto> GetTaskDetail(string id)
        {
            string url = $"/api/Todos/{id}";
            var result = await _httpClient.GetFromJsonAsync<TodoDto>(url);
            return result;
        }

        public Task<PagedList<TodoDto>> GetTaskList(TaskListSearch taskListSearch)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<TodoDto>> GetTaskList1(TaskListSearch taskListSearch)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = taskListSearch.PageNumber.ToString()
            };

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                queryStringParam.Add("name", taskListSearch.Name);
            if (taskListSearch.AssigneeId.HasValue)
                queryStringParam.Add("assigneeId", taskListSearch.AssigneeId.ToString());
            if (taskListSearch.Priority.HasValue)
                queryStringParam.Add("priority", taskListSearch.Priority.ToString());

            string url = QueryHelpers.AddQueryString("/api/Todos", queryStringParam);

            /*User = await _httpService.Post< PagedList<TodoDto>("/api/Login", taskListSearch);*/

           /* var result = await _httpClient.GetFromJsonAsync<PagedList<TodoDto>>(url);*/
            var result = await _httpService.Get<PagedList<TodoDto>>(url);
            return result;
        }

        public async Task<bool> UpdateTask(Guid id, TodoUpdateRequest request)
        {
            var result = await _httpClient.PostAsJsonAsync("/api/Todos", request);
            return result.IsSuccessStatusCode;
        }
    }
}
