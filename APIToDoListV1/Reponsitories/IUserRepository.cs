using APIToDoListV1.Entities;
using Model.UserModel;

namespace APIToDoListV1.Reponsitories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUserList();
        public Task<User> Create(User user);
        public Task<User> GetUserById(Guid id);

        public User GetUserByNameAndPassword(LoginUser loginUser );
        public User GetUserbyUserName(string  UserName );
    }
}
