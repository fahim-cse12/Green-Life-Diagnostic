using Entities.Models;

namespace Contracts
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorAsync(bool trackChanges);
        Task<Doctor> GetDoctorAsync(Guid doctorId, bool trackChanges);
        void CreateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
        void UpdateDoctor(Doctor doctor);
    }
}
