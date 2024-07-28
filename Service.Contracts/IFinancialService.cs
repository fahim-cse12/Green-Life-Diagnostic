using Entities.Responses;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IFinancialService
    {
        Task<ApiBaseResponse> GetAllFinanceRecordAsync(bool trackChanges);
        Task<ApiBaseResponse> GetFinanceRecordAsync(Guid FinanceRecordId, bool trackChanges);
        Task<ApiBaseResponse> CreateFinanceRecordAsync(FinanceRecordCreateDto FinanceRecordDto);
        Task<ApiBaseResponse> DeleteFinanceRecordAsync(Guid FinanceRecordId, bool trackChanges);
        Task<ApiBaseResponse> UpdateFinanceRecordAsync(Guid FinanceRecordId, FinanceRecordCreateDto FinanceRecordDto, bool trackChanges);
    }
}
