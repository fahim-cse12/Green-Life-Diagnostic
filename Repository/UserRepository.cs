using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateUSer(User user)
        {
           Create(user);    
        }

        public void DeleteUser(User user)
        {
            Delete(user);   
        }

        public async Task<IEnumerable<User>> GetAllUserAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid userId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(userId), trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }
    }
}
