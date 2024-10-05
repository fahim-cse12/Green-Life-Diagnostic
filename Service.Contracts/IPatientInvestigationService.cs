using Entities.Responses;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IPatientInvestigationService
    {
        Task<ApiBaseResponse> GetAllInvestigationAsync(bool trackChanges);
        Task<ApiBaseResponse> GetInvestigationAsync(Guid investigationId, bool trackChanges);
        Task<ApiBaseResponse> CreateInvestigationAsync(InvestigationCreateDto investigationDto);
        Task<ApiBaseResponse> DeleteInvestigationAsync(Guid investigationId, bool trackChanges);
        Task<ApiBaseResponse> UpdateInvestigationAsync(Guid investigationId, InvestigationCreateDto investigationDto, bool trackChanges);
    }
}
