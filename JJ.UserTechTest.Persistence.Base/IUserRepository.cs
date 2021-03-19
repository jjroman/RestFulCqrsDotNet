using System.Collections.Generic;
using System.Threading.Tasks;
using JJ.UserTechTest.Models;

namespace JJ.UserTechTest.Persistence.Base
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(int userId);
        Task<bool> ExistsAsync(int userId);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<User> DeleteAsync(int userId);
        
    }
}