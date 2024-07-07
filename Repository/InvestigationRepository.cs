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
    public class InvestigationRepository : RepositoryBase<Investigation>, IInvestigationRepository
    {
        public InvestigationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateInvestigation(Investigation investigation)
        {
            Create(investigation);
        }

        public void DeleteInvestigation(Investigation investigation)
        {
            Delete(investigation);  
        }

        public async Task<IEnumerable<Investigation>> GetAllInvestigationAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(i=> i.Id).ToListAsync(); 
        }

        public async Task<Investigation> GetInvestigationAsync(Guid investigationId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(investigationId), trackChanges).SingleOrDefaultAsync();
        }
    }
}
