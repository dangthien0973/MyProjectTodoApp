using APIToDoListV1.Reponsitories;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace APIToDoListV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UsersController( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var ListUser = await _userRepository.GetUserList();
            var assignees = ListUser.Select(x => new AssigneeDto()
            {
                FullName = x.FirstName,
                Id = x.Id,
            });
            return Ok(assignees);
        }
    }
}
