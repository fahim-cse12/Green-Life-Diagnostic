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
        Task<ApiBaseResponse> GetFilteredPatientInvestigationsAsync( string? patientInvestigationUniqueId, 
                                                                     string? patientUniqueId, 
                                                                     string? patientName,
                                                                     string? patientMobileNo,
                                                                     DateTime? fromDate,
                                                                     DateTime? toDate,
                                                                     int pageNumber,
                                                                     int pageSize,
                                                                     bool trackChange);
        Task<ApiBaseResponse> CreatePatientInvestigationAsync(PatientInvestigationCreateDto patientInvestigationCreateDto); 
        Task<ApiBaseResponse> UpdatePatientInvestigationAsync(PatientInvestigationDto patientInvestigationDto);
        Task<ApiBaseResponse> DeletePatientInvestigationDetailAsync(Guid detailId);
    }
}
