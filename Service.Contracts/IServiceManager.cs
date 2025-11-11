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
        IFinancialReportService financialReportService { get; }
        IUserService userService { get; }
        IAuthenticationService authenticationService { get; }
        IDashboardService dashboardService { get; }
        IRoleService roleService { get; }
    }
}
