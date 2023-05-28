using APIToDoListV1.Entities;
using Model.UserModel;

namespace APIToDoListV1.Utils
{
    public interface IJwtUtils
    {
        public string GenerateToken (User registerUser);

        public bool ValidateToken (string token);

    }
}
