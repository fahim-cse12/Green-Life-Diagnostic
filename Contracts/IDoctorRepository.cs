using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllDoctorAsync(bool trackChanges);
        Task<Doctor> GetDoctorAsync(Guid doctorId, bool trackChanges);
        void CreateDoctor(Doctor doctor);
        void DeleteDoctor(Doctor doctor);
    }
}
