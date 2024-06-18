using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IDoctorRepository> _doctorRepository;
        private readonly Lazy<IPatientRepository> _patientRepostory;
        private readonly Lazy<IPatientInvestigationRepository> _patientInvestigationRepository;
        private readonly Lazy<ITicketRepository> _ticketRepository;
        private readonly Lazy<IInvestigationRepository> _investigationRepository;
        private readonly Lazy<IFinancialRepository> _financialRepository;
        private readonly Lazy<IUserRepository> _userRepository;
      
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _doctorRepository = new Lazy<IDoctorRepository>(() => new DoctorRepository(repositoryContext));
            _patientRepostory = new Lazy<IPatientRepository>(() => new PatientRepository(repositoryContext));
            _patientInvestigationRepository = new Lazy<IPatientInvestigationRepository>(() => new PatientInvestigationRepository(repositoryContext));
            _ticketRepository = new Lazy<ITicketRepository>(() => new TicketRepository(repositoryContext));
            _investigationRepository = new Lazy<IInvestigationRepository>(() => new InvestigationRepository(repositoryContext));
            _financialRepository = new Lazy<IFinancialRepository>(() => new FinancialRecordRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
        }

        public IDoctorRepository Doctor => _doctorRepository.Value;

        public IPatientRepository Patient => _patientRepostory.Value;

        public IPatientInvestigationRepository PatientInvestigation => _patientInvestigationRepository.Value;

        public ITicketRepository Ticket => _ticketRepository.Value;

        public IInvestigationRepository Investigation => _investigationRepository.Value;

        public IFinancialRepository FinancialRecord => _financialRepository.Value;

        public IUserRepository User => _userRepository.Value;

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
