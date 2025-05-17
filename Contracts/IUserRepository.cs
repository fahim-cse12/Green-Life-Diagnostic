using Entities.Models;

namespace Contracts
{
    public interface IUserRepository 
    {
        Task<IEnumerable<User>> GetAllUserAsync(bool trackChanges);
        Task<User> GetUserAsync(Guid userId, bool trackChanges);
        void CreateUSer(User user);
        
        void DeleteUser(User user);
        void UpdateUser(User user);
    }
}
