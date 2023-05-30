using Model.UserModel;

namespace MyProjectTodoApp.Services
{
    public interface ILogin
    {
        public Task LoginUser(LoginUser loginUser);
    }
}
