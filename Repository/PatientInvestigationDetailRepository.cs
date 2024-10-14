using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
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
    }
}
