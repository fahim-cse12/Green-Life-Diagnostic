using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class FinancialRecordRepository : RepositoryBase<FinancialRecord>, IFinancialRepository
    {
        public FinancialRecordRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreateFinancialRecord(FinancialRecord financialRecord)
        {
            Create(financialRecord);
        }

        public void DeleteFinancialRecord(FinancialRecord financialRecord)
        {
           Delete(financialRecord); 
        }
        public async Task<IEnumerable<FinancialRecord>> GetAllFinancialRecordAsync(bool trackChanges) =>
                                                       await FindAll(trackChanges).Where(i => i.Status == true)
                                                       .OrderBy(c => c.CreatedAt)
                                                       .ToListAsync();

        public async Task<FinancialRecord> GetFinancialRecordAsync(Guid financialRecordId, bool trackChanges)
        {
           return await FindByCondition(x=> x.Id.Equals(financialRecordId), trackChanges).SingleOrDefaultAsync();
        }

        public void UpdateFinancialRecord(FinancialRecord financialRecord)
        {
            Update(financialRecord);
        }
    }
}
