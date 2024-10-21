using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PatientInvestigationDetailRepository : RepositoryBase<PatientInvestigationDetail>, IPatientInvestigationDetailRepository
    {
        public PatientInvestigationDetailRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        { }

        public void CreatePatientInvestigationDetails(List<PatientInvestigationDetail> patientInvestigationDetails)
        {
            CreateRange(patientInvestigationDetails);   
        }

        public void DeleteSinglePatientInvestigationDetails(PatientInvestigationDetail singlePatientInvestigationDetails)
        {
            Delete(singlePatientInvestigationDetails);
        }

        public async Task<List<PatientInvestigationDetail>> GetInvestigationDetailByPatientInvestigationId(Guid patientInvestigationId, bool trackChanges)
        {
            return await FindByCondition(i => i.PatientInvestigationId.Equals(patientInvestigationId), trackChanges).ToListAsync();
        }

        public async Task<PatientInvestigationDetail> GetPatientInvestigationDetailById(Guid patientInvestigationDetailId, bool trackChanges)
        {
            
            return await FindByCondition(i => i.PatientInvestigationDetailId.Equals(patientInvestigationDetailId), trackChanges).SingleOrDefaultAsync();
                      
        }

        public void UpdatePatientInvestigationDetails(List<PatientInvestigationDetail> patientInvestigationDetails)
        {
            UpdateRange(patientInvestigationDetails);   
        }

        public void UpdateSinglePatientInvestigationDetails(PatientInvestigationDetail singlePatientInvestigationDetails)
        {
            Update(singlePatientInvestigationDetails);
        }
    }
}
