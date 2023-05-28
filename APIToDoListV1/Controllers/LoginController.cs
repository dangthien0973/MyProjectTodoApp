using APIToDoListV1.Reponsitories;
using APIToDoListV1.Utils;
using Microsoft.AspNetCore.Mvc;
using Model.UserModel;

namespace APIToDoListV1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly ILoginRepository i_login;

        public LoginController(IUserRepository userRepository, IJwtUtils jwtUtils, ILoginRepository login)
        {

            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            i_login = login;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUser login)
        {
            return Ok(i_login.LoginUser(login));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            var result = i_login.GetRegisterUser(registerUser);
            if (!result) return BadRequest("UserName already taken , Please use another name ");
            return Ok(result);
        }
    }
}