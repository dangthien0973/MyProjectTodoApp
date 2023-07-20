using APIToDoListV1.Entities;
using Model;
using Model.SeekWork;

namespace APIToDoListV1.Reponsitories
{
  public interface IdotoRepository
    {
        Task<PagedList<Todo>> GetTaskList(TaskListSearch taskListSearch);

        Task<Todo> Create(Todo task);

        Task<Todo> Update(Todo task);

        Task<Todo> Delete(Todo task);

        Task<Todo> GetById(Guid id);
    }
}
