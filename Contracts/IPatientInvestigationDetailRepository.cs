using Entities.Models;

namespace Contracts
{
    public interface IPatientInvestigationDetailRepository
    {
        void CreatePatientInvestigationDetails(List<PatientInvestigationDetail> patientInvestigationDetails);
        void CreateSinglePatientInvestigationDetails(PatientInvestigationDetail patientInvestigationDetails);
        void UpdatePatientInvestigationDetails(List<PatientInvestigationDetail> patientInvestigationDetails);
        void UpdateSinglePatientInvestigationDetails(PatientInvestigationDetail singlePatientInvestigationDetails);
        void DeleteSinglePatientInvestigationDetails(PatientInvestigationDetail singlePatientInvestigationDetails);
        Task<List<PatientInvestigationDetail>> GetInvestigationDetailByPatientInvestigationId(Guid patientInvestigationId, bool trackChanges);
        Task<PatientInvestigationDetail> GetPatientInvestigationDetailById(Guid patientInvestigationDetailId, bool trackChanges);

    }
}
