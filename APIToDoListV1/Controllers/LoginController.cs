using APIToDoListV1.Reponsitories;
using APIToDoListV1.Utils;
using Microsoft.AspNetCore.Authorization;
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
            try
            {
                var result = i_login.LoginUser(login);
                if (result == null || string.IsNullOrEmpty(result.Token)) return BadRequest();
                return Ok(result);
            }
            catch(Exception ex)
            {
                ExteptionUtils exteptionUtils=new  ExteptionUtils(ex.ToString());
                return BadRequest("Something was wrong went login user Please try again");
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = i_login.GetRegisterUser(registerUser);
                if (!result) return BadRequest("UserName already taken , Please use another name ");
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest("Some thing went wrong when register . Please try again! ");

            }
        }
    }
}