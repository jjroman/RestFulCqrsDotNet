using System.Collections.Generic;
using System.Threading.Tasks;
using Innocv.UserTechTest.Models;
using Innocv.UserTechTest.Persistence.Base;
using Microsoft.EntityFrameworkCore;

namespace Innocv.UserTechTest.Persistence.InMemory
{
    public class UserRepositoryInMemory : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepositoryInMemory(UserDbContext context)
        {
            _context = context;
        }

        // QUERIES
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(int userId)
        {
            return await _context.Users.SingleAsync(u => u.Id == userId);
        }

        public async Task<bool> ExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }


        // COMMANDS
        public async Task<User> AddAsync(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> DeleteAsync(int userId)
        {
            var user = await GetAsync(userId);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}