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
        public void UpdatePatientInvestigation(PatientInvestigation patientInvestigation)
        {
            Update(patientInvestigation);
        }

        public IQueryable<PatientInvestigation> GetAllInvestigations(bool trackChanges)
        {
            return FindAll(trackChanges);
        }

        public async Task<PatientInvestigation> GetPatientInvestigationById(Guid id, bool trackChanges)
        {
            return await FindByCondition(i => i.PatientInvestigationId.Equals(id), trackChanges).SingleOrDefaultAsync();
        }
    }
}
