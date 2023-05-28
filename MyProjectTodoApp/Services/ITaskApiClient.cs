using Model;
using Model.SeekWork;

namespace MyProjectTodoApp.Services
{
    public interface ITaskApiClient
    {
        Task<PagedList<TodoDto>> GetTaskList(TaskListSearch taskListSearch);
        Task<PagedList<TodoDto>> GetTaskList1(TaskListSearch taskListSearch);

        Task<PagedList<TodoDto>> GetMyTasks(TaskListSearch taskListSearch);


        Task<TodoDto> GetTaskDetail(string id);

        Task<bool> CreateTask(TodoCreateRequest request);
        Task<bool> UpdateTask(Guid id, TodoUpdateRequest request);


        Task<TodoDto> DeleteTask(string id);
    }
}
