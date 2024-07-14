using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IDoctorService> _doctorService;
        private readonly Lazy<IPatientService> _patientService;
        private readonly Lazy<IPatientInvestigationService> _patientInvesitgationService;
        private readonly Lazy<IInvestigationService> _investigationService;
        private readonly Lazy<ITicketService> _ticketService;
        private readonly Lazy<IFinancialService> _financialService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger,
              IMapper mapper, UserManager<User> userManager, IOptions<JwtConfiguration> configuration)
        {
            _doctorService = new Lazy<IDoctorService>(() => new DoctorService(repositoryManager, logger, mapper));
            _patientService = new Lazy<IPatientService>(() => new PatientService(repositoryManager, logger, mapper));
            _patientInvesitgationService = new Lazy<IPatientInvestigationService>(() => new PatientInvestigationService(repositoryManager, logger, mapper));
            _investigationService = new Lazy<IInvestigationService>(() => new InvestigationService(repositoryManager, logger, mapper));
            _ticketService = new Lazy<ITicketService>(() => new TicketService(repositoryManager, logger, mapper));
            _financialService = new Lazy<IFinancialService>(() => new FinancialRecordService(repositoryManager, logger, mapper));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
        }
        public IDoctorService doctorService => _doctorService.Value;
        public IPatientService patientService => _patientService.Value;
        public IPatientInvestigationService patientInvestigationService => _patientInvesitgationService.Value;
        public IInvestigationService investigationService => _investigationService.Value; 
        public ITicketService ticketService => _ticketService.Value;
        public IFinancialService financialService => _financialService.Value;
        public IUserService userService => _userService.Value;  
        public IAuthenticationService authenticationService => _authenticationService.Value;

        
    }
}
