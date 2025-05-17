using Entities.Models;

namespace Contracts
{
    public interface IPatientInvestigationRepository
    {
        void CreatePatientInvestigation(PatientInvestigation patientInvestigation);
        IQueryable<PatientInvestigation> GetAllInvestigations(bool trackChanges);
        Task<PatientInvestigation> GetPatientInvestigationById(Guid id, bool trackChanges);    
        void UpdatePatientInvestigation(PatientInvestigation patientInvestigation);
    }
}
