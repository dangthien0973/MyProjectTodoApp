using Model.UserModel;

namespace APIToDoListV1.Reponsitories
{
    public interface ILoginRepository
    {
        public LoginReponse LoginUser(LoginUser registerUser);
        public Task<bool> Logout();
        
        public bool GetRegisterUser(RegisterUser registerUser);
    }
}
