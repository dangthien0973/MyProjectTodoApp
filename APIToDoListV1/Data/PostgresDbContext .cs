
using APIToDoListV1.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppChat_API.Data
{
    public class PostgresDbContext : IdentityDbContext<User, Role, Guid>
    {
        public PostgresDbContext()
        {
        }
        public PostgresDbContext(DbContextOptions<PostgresDbContext> options)
         : base(options)
        {

        }

        public DbSet<Todo> Todo { set; get; }
        public DbSet<User>  User { set; get; }
        public DbSet<Role> Role { set; get; }
 
    }
}