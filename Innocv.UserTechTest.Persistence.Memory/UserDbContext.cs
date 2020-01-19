using Innocv.UserTechTest.Models;
using Microsoft.EntityFrameworkCore;

namespace Innocv.UserTechTest.Persistence.InMemory
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}