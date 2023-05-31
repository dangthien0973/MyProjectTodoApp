using APIToDoListV1.Utils;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Model.UserModel;

namespace APIToDoListV1.Reponsitories
{
    public class LoginRepository : ILoginRepository
    {
        private IJwtUtils _jwtUtils;
        private IUserRepository _userRepository;
        
        public LoginRepository(IJwtUtils jwtUtils, IUserRepository userRepository)
        {
            _jwtUtils = jwtUtils;
            _userRepository = userRepository;
        }
        public  bool GetRegisterUser(RegisterUser registerUser)
        {
            try
            {
                var user = _userRepository.GetUserbyUserName(registerUser.Username);
                if (user != null) return false;

                var UserCreate = _userRepository.Create(new Entities.User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName,
                    Email = registerUser.Email,
                    NormalizedEmail = registerUser.Email,
                    PhoneNumber = registerUser.PhoneNumbers,
                    UserName = registerUser.Username,
                    NormalizedUserName = registerUser.Username,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerUser.Password)
                }); 
                if (UserCreate != null)  return true;
                

            }
            catch(Exception ex)  
            {
                // handle exeption
                return false;
            }
            return false;
            
        }

        public Task<bool> Logout()
        {
            throw new NotImplementedException();
        }

        public  LoginReponse LoginUser(LoginUser  lginUser)
        {
            string token = "";
            var user =  _userRepository.GetUserByNameAndPassword(lginUser);
            if (user == null) return null;
            token = _jwtUtils.GenerateToken(user);
            if (token == null || string.IsNullOrEmpty(token)) return null;
            return new LoginReponse()
            {
               username = user.UserName,
               FullName =$"{ user.FirstName} {user.LastName}",
               Token = token
            };
        }
    }
}
