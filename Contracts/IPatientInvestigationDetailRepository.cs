using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPatientInvestigationDetailRepository
    {
        void CreatePatientInvestigationDetails(List<PatientInvestigationDetail> patientInvestigationDetails);
        void CreateSinglePatientInvestigationDetails(PatientInvestigationDetail patientInvestigationDetails);
        void UpdatePatientInvestigationDetails(List<PatientInvestigationDetail> patientInvestigationDetails);
        void UpdateSinglePatientInvestigationDetails(PatientInvestigationDetail singlePatientInvestigationDetails);
        void DeleteSinglePatientInvestigationDetails(PatientInvestigationDetail singlePatientInvestigationDetails);
        Task<List<PatientInvestigationDetail>> GetInvestigationDetailByPatientInvestigationId(Guid patientInvestigationId, bool trackChanges);
        Task<PatientInvestigationDetail> GetPatientInvestigationDetailById(Guid patientInvestigationDetailId, bool trackChanges);

    }
}
