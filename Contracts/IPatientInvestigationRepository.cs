﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPatientInvestigationRepository
    {
        void CreatePatientInvestigation(PatientInvestigation patientInvestigation);
        IQueryable<PatientInvestigation> GetAllInvestigations(bool trackChanges);
        Task<PatientInvestigation> GetPatientInvestigationById(Guid id, bool trackChanges);    
        void UpdatePatientInvestigation(PatientInvestigation patientInvestigation);
    }
}
