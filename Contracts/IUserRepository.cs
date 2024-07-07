using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository 
    {
        Task<IEnumerable<User>> GetAllUserAsync(bool trackChanges);
        Task<User> GetUserAsync(Guid userId, bool trackChanges);
        void CreateUSer(User user);
        
        void DeleteUser(User user);
    }
}
