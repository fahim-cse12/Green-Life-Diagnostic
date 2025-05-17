using Entities.Models;

namespace Contracts
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync(bool trackChanges);
        Task<Patient> GetPatientAsync(Guid patientId, bool trackChanges);
        void CreatePatient(Patient patient);
        void DeletePatient(Patient patient);
        void UpdatePatient(Patient patient);
    }
}
