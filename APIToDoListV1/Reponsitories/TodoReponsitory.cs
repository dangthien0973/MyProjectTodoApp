using APIToDoListV1.Entities;
using AppChat_API.Data;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.SeekWork;

namespace APIToDoListV1.Reponsitories
{
    public class TodoReponsitory : IdotoRepository
    {
        private readonly PostgresDbContext _context; // private only không thể thay đổi trong quá trình chạy ứng dụng 

        public TodoReponsitory(PostgresDbContext context)
        {
            _context = context;
        }
        public async Task<Todo> Create(Todo task)
        {
            _context.Todo.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Todo> Delete(Todo task)
        {
            _context.Todo.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public  async Task<Todo> GetById(Guid id)
        {
            return await _context.Todo.FindAsync(id);
        }

        public async Task<PagedList<Todo>> GetTaskList(TaskListSearch taskListSearch)
        {
            var query = await _context.Todo.ToListAsync();

            if (!string.IsNullOrEmpty(taskListSearch.Name))
                query = query.Where(x=> x.Name == taskListSearch.Name).ToList();

            if (taskListSearch.AssigneeId.HasValue)
                query = query.Where(x => x.AssigneeId == taskListSearch.AssigneeId).ToList();

            if (taskListSearch.Priority.HasValue)
                query = query.Where(x => x.Priority == taskListSearch.Priority).ToList();
            var count = query.Count;
            query = query.Skip((taskListSearch.PageNumber - 1) * taskListSearch.PageSize).Take(taskListSearch.PageSize).ToList();

            
            
            return new PagedList<Todo>(query, count, taskListSearch.PageNumber, taskListSearch.PageSize);

        }

        public async Task<Todo> Update(Todo task)
        {
            _context.Todo.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
