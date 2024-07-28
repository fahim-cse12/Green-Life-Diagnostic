using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PatientRepository : RepositoryBase<Patient>, IPatientRepository
    {
        public PatientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreatePatient(Patient patient)
        {
            Create(patient);
        }

        public void DeletePatient(Patient patient)
        {
            Delete(patient);    
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<Patient> GetPatientAsync(Guid patientId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(patientId), trackChanges).SingleOrDefaultAsync();
        }

        public void UpdatePatienc(Patient patient)
        {
            Update(patient);    
        }
    }
}
