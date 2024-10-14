using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPatientInvestigationRepository
    {
        //Task<IEnumerable<PatientInvestigation>> GetAllPatientInvestigationAsync(bool trackChanges);
        //Task<PatientInvestigation> GetPatientInvestigationAsync(Guid patientInvestigationId, bool trackChanges);
        void CreatePatientInvestigation(PatientInvestigation patientInvestigation);

        //void DeletePatientInvestigation(PatientInvestigation investigation);
        //void UpdatePatientInvestigation(PatientInvestigation investigation);

    }
}
