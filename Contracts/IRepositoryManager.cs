using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IDoctorRepository Doctor { get; }
        IPatientRepository Patient { get; }
        IPatientInvestigationRepository PatientInvestigation { get; }
        ITicketRepository Ticket { get; } 
        IInvestigationRepository Investigation { get; }
        IFinancialRepository FinancialRecord { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
