using APIToDoListV1.Entities;
using AppChat_API.Data;
using Microsoft.AspNetCore.Identity;
using Model.Enums;

namespace APIToDoListV1.Data
{
    public class TodoListDbContextSeed
    {

        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public async Task SeedAsync(PostgresDbContext context, ILogger<TodoListDbContextSeed> logger)
        {
            Guid GuidId = Guid.NewGuid();
            if (!context.Users.Any())
            {
                var user = new User()
                {
                    Id = GuidId,
                    FirstName = "Mr",
                    LastName = "A",
                    Email = "admin1@gmail.com",
                    NormalizedEmail = "ADMIN1@GMAIL.COM",
                    PhoneNumber = "032132131",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                user.PasswordHash = _passwordHasher.HashPassword(user, "Admin@123$");
                context.Users.Add(user);
            }
            if (!context.Todo.Any())
            {
                context.Todo.Add(new Entities.Todo()
                {
                    Id = GuidId,
                    Name = "Same tasks 1",
                    CreatedDate = DateTime.UtcNow,
                    Priority = Priority.High,
                    Status = Status.Open,
                    AssigneeId = GuidId
                });
            }

            await context.SaveChangesAsync();

        }
    }
}
