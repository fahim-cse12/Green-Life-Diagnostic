using Entities.Responses;
using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface IInvestigationService
    {
        Task<ApiBaseResponse> GetAllInvestigationAsync(bool trackChanges);
        Task<ApiBaseResponse> GetInvestigationAsync(Guid investigationId, bool trackChanges);
        Task<ApiBaseResponse> CreateInvestigationAsync(InvestigationCreateDto investigationDto);
        Task<ApiBaseResponse> DeleteInvestigationAsync(Guid investigationId, bool trackChanges);
        Task<ApiBaseResponse> UpdateInvestigationAsync(Guid investigationId, InvestigationCreateDto investigationDto, bool trackChanges);
    }
}
