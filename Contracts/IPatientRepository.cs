﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync(bool trackChanges);
        Task<Patient> GetPatientAsync(Guid patientId, bool trackChanges);
        void CreatePatient(Patient patient);
        Task<IEnumerable<Patient>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        void DeletePatient(Patient patient);
    }
}
