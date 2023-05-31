using APIToDoListV1.Entities;
using APIToDoListV1.Reponsitories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Enums;
using Model.SeekWork;
using System.Linq;

namespace APIToDoListV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IdotoRepository _todoRepository;
        private readonly IUserRepository _userRepository;

        public TodosController(IdotoRepository idotoRepository, IUserRepository userRepository)
        {
            _todoRepository = idotoRepository;
            _userRepository = userRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTodo([FromQuery]TaskListSearch taskListSearch)
        {
            var listTodo = await _todoRepository.GetTaskList(taskListSearch);
            var tastDto = listTodo.Items.Select( x=> new TodoDto()
            {

                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Id = x.Id,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A"
            });
            return Ok(new PagedList<TodoDto>(tastDto.ToList(), listTodo.MetaData.TotalCount, listTodo.MetaData.CurrentPage, listTodo.MetaData.PageSize) );
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var User = await _userRepository.Create(new Entities.User()
            {
                Id= Guid.NewGuid(),
                FirstName = "Mr2",
                LastName = "A2",
                Email = "admin12@gmail.com",
                NormalizedEmail = "ADMIN12@GMAIL.COM",
                PhoneNumber = "0321232131",
                UserName = new Random().Next(3,1020).ToString(),
                NormalizedUserName = new Random().Next(3, 1020).ToString(),
                SecurityStamp = Guid.NewGuid().ToString(),
                

            });
            var task = await _todoRepository.Create(new Entities.Todo()
            {
                Name = request.Name,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open,
                CreatedDate = DateTime.UtcNow,
                Id = request.Id,
                AssigneeId= User.Id

            });
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TodoUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _todoRepository.GetById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.Name = request.Name;
            taskFromDb.Priority = request.Priority;

            var taskResult = await _todoRepository.Update(taskFromDb);

            return Ok(new TodoDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var task = await _todoRepository.GetById(id);
            if (task == null) return NotFound($"{id} is not found");
            return Ok(new TodoDto()
            {
                Name = task.Name,
                Status = task.Status,
                Id = task.Id,
                AssigneeId = task.AssigneeId,
                Priority = task.Priority,
                CreatedDate = task.CreatedDate
            });
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var taskResult = await _todoRepository.GetById(id);
            if (taskResult == null) return NotFound($"{id} is not found");
            var result = await _todoRepository.Delete(taskResult);
            return Ok(new TodoDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });
        }

    }
}
