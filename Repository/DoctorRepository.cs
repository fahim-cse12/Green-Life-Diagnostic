using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(RepositoryContext repositoryContext): base(repositoryContext)
        { }

        public void CreateDoctor(Doctor doctor)
        {
            Create(doctor);
        }

        public void DeleteDoctor(Doctor doctor)
        {
            Delete(doctor);
        }

        public async Task<IEnumerable<Doctor>> GetAllDoctorAsync(bool trackChanges) =>
                                                          await FindAll(trackChanges)
                                                          .OrderBy(c => c.Name)
                                                          .ToListAsync();

        //public async Task<IEnumerable<Doctor>> GetAllDoctorAsync(bool trackChanges)
        //{
        //   return  await FindAll(trackChanges).OrderBy(x => x.Id).ToListAsync();
        //}

        public async Task<Doctor> GetDoctorAsync(Guid doctorId, bool trackChanges)
        {
           return  await FindByCondition(i=> i.Id.Equals(doctorId), trackChanges).SingleOrDefaultAsync();
        }
    }
}
