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
            try
            {
                return await FindAll(trackChanges).Where(i => i.Status == true)
                                                         .OrderBy(c => c.InvestigationName)
                                                         .ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }

           
        }
        
                                                         
        public async Task<Investigation> GetInvestigationAsync(Guid investigationId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(investigationId), trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateInvestigation(Investigation investigation)
        {
            Update(investigation);
        }
    }
}
