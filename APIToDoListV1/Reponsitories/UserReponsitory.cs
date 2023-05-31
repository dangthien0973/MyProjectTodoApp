using APIToDoListV1.Entities;
using AppChat_API.Data;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.UserModel;

namespace APIToDoListV1.Reponsitories
{
    public class UserReponsitory :  IUserRepository
    {
        private readonly PostgresDbContext _context; // private only không thể thay đổi trong quá trình chạy ứng dụng 

        public UserReponsitory(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
           await _context.AddAsync(user);
           await _context.SaveChangesAsync();
           return user;
        }

        public  async Task<User> GetUserById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
        public User GetUserbyUserName(string userName)
        {
            var User = new User();
            User = _context.Users.SingleOrDefault(x => x.UserName.Trim() == userName);
            return User;
        }
        public  User GetUserByNameAndPassword(LoginUser loginUser)
        {
            var User = new User();
            User = GetUserbyUserName(loginUser.UserName);

            if (User != null && !BCrypt.Net.BCrypt.Verify(loginUser.Password, User.PasswordHash))
                return null;

            return User;    
        }

        public async Task<List<User>> GetUserList()
        {
          return await _context.Users.ToListAsync();
        }
        public IdentityRole<Guid> AssignRoleIntoUser(User user)
        {
            return new IdentityRole<Guid>();
        }
    }
}
