using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<FinancialRecord> FindFinancialRecordsByConditionAsync(Expression<Func<FinancialRecord, bool>> condition, bool trackChanges)
        {
            return await FindByCondition(condition, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<FinancialRecord>> GetAllFinancialRecordAsync(bool trackChanges) =>
                                                       await FindAll(trackChanges).Where(i => i.Status == true)
                                                       .OrderBy(c => c.CreatedAt)
                                                       .ToListAsync();

        public async Task<FinancialRecord> GetFinancialRecordAsync(Guid financialRecordId, bool trackChanges)
        {
           return await FindByCondition(x=> x.Id.Equals(financialRecordId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<FinancialRecord> GetFinancialRecordsByConditionAsync(Expression<Func<FinancialRecord, bool>> condition, bool trackChanges)
        {
            return await FindByConditionAsync(condition, trackChanges);
        }

        public void UpdateFinancialRecord(FinancialRecord financialRecord)
        {
            Update(financialRecord);
        }
    }
}
