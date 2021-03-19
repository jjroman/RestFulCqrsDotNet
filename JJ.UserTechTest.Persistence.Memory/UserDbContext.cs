using JJ.UserTechTest.Models;
using Microsoft.EntityFrameworkCore;

namespace JJ.UserTechTest.Persistence.InMemory
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}