using APIToDoListV1.Entities;
using APIToDoListV1.Reponsitories;
using APIToDoListV1.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Enums;
using Model.SeekWork;
using System.Linq;
using System.Security.Claims;

namespace APIToDoListV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodosController : ControllerBase
    {
        private readonly IdotoRepository _todoRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public TodosController(IdotoRepository idotoRepository, IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _todoRepository = idotoRepository;
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

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
        [Authorize(Policy = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var task = await _todoRepository.Create(new Entities.Todo()
            {
                Name = request.Name,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open,
                CreatedDate = DateTime.UtcNow,
                Id = request.Id,
                AssigneeId= request.UserId

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
