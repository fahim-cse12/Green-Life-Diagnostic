using Entities.Responses;
using Shared.DataTransferObject;

namespace Service.Contracts
{
    public interface IPatientInvestigationService
    {
        Task<ApiBaseResponse> GetFilteredPatientInvestigationsAsync( string? patientInvestigationUniqueId, 
                                                                     string? patientUniqueId, 
                                                                     string? patientName,
                                                                     string? patientMobileNo,
                                                                     DateTime? fromDate,
                                                                     DateTime? toDate,
                                                                     int pageNumber,
                                                                     int pageSize,
                                                                     bool trackChange);
        Task<ApiBaseResponse> GetPatientInvestigationAsync(Guid patientInvestigationId, bool trackChanges);

        Task<ApiBaseResponse> CreatePatientInvestigationAsync(PatientInvestigationCreateDto patientInvestigationCreateDto); 
        Task<ApiBaseResponse> UpdatePatientInvestigationAsync(PatientInvestigationUpdateDto patientInvestigationUpdateDto);
        Task<ApiBaseResponse> DeletePatientInvestigationDetailAsync(Guid patientInvestigaionId,Guid detailId);
    }
}
