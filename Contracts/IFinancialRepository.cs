using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFinancialRepository
    {
        Task<IEnumerable<FinancialRecord>> GetAllFinancialRecordAsync(bool trackChanges);
        Task<FinancialRecord> GetFinancialRecordAsync(Guid financialRecordId, bool trackChanges);
        public Task<FinancialRecord> FindFinancialRecordsByConditionAsync(Expression<Func<FinancialRecord, bool>> condition, bool trackChanges);
        public Task<FinancialRecord> GetFinancialRecordsByConditionAsync(Expression<Func<FinancialRecord, bool>> condition, bool trackChanges);
        void CreateFinancialRecord(FinancialRecord financialRecord);
        //Task<IEnumerable<FinancialRecord>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeleteFinancialRecord(FinancialRecord financialRecord);
        void UpdateFinancialRecord(FinancialRecord financialRecord);
    }
}
