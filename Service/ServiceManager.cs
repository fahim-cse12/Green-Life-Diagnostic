using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using FluentValidation;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
        private readonly IOptions<JwtConfiguration> _configuration;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IHttpContextAccessor _contextAccessor;
        public ServiceManager(
            IRepositoryManager repositoryManager,
            ILoggerManager logger,
            IMapper mapper,
            UserManager<User> userManager,
            IOptions<JwtConfiguration> configuration,            
            IServiceProvider serviceProvider,
            IHttpContextAccessor contextAccessor)
        {
            _doctorService = new Lazy<IDoctorService>(() => new DoctorService(
                repositoryManager,
                logger,
                mapper,
                serviceProvider.GetRequiredService<IValidator<Doctor>>()));

            _patientService = new Lazy<IPatientService>(() => new PatientService(
                repositoryManager,
                logger,
                mapper,
                serviceProvider.GetRequiredService<IValidator<Patient>>()));

            _patientInvesitgationService = new Lazy<IPatientInvestigationService>(() => new PatientInvestigationService(
                repositoryManager, logger, mapper, serviceProvider.GetRequiredService<IValidator<PatientInvestigation>>(), contextAccessor));
            _investigationService = new Lazy<IInvestigationService>(() => new InvestigationService(
                repositoryManager, 
                logger,
                mapper,
                serviceProvider.GetRequiredService<IValidator<Investigation>>()));
            _ticketService = new Lazy<ITicketService>(() => new TicketService(repositoryManager, logger, mapper));
            _financialService = new Lazy<IFinancialService>(() => new FinancialRecordService(
                repositoryManager,
                logger,
                mapper,
                serviceProvider.GetRequiredService<IValidator<FinancialRecord>>()));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
            _configuration = configuration;
            _jwtConfiguration = _configuration.Value;
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
