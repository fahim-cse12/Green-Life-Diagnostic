using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IDoctorService doctorService { get; }
        IPatientService patientService { get; }
        IPatientInvestigationService patientInvestigationService { get; }
        IInvestigationService investigationService { get; }
        ITicketService ticketService { get; }   
        IFinancialService financialService { get; }
        IUserService userService { get; }
        IAuthenticationService authenticationService { get; }
    }
}
