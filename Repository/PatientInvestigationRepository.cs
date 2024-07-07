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
    public class PatientInvestigationRepository : RepositoryBase<PatientInvestigation>, IPatientInvestigationRepository
    {
        public PatientInvestigationRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreatePatientInvestigation(PatientInvestigation patientInvestigation)
        {
            Create(patientInvestigation);
        }

        public void DeletePatientInvestigation(PatientInvestigation patientInvestigation)
        {
           Delete(patientInvestigation);    
        }

        public async Task<IEnumerable<PatientInvestigation>> GetAllPatientInvestigationAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<PatientInvestigation> GetPatientInvestigationAsync(Guid patientInvestigationId, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(patientInvestigationId), trackChanges).SingleOrDefaultAsync();
        }
    }
}
