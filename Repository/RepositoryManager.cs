using Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
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
        private IDbContextTransaction _transaction;

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

        public async Task BeginTransaction(CancellationToken cancellationToken)
        {
            _transaction = await _repositoryContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransaction(CancellationToken cancellationToken)
        {
            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            finally
            {

                await _transaction.DisposeAsync();
            }
        }

        public async Task ExecuteFromSqlRaw(string query, CancellationToken cancellationToken, SqlParameterExpression[] parameters = null)
        {
            await _repositoryContext.Database.ExecuteSqlRawAsync(query, cancellationToken, parameters);
        }

        public IQueryable<dynamic> GetFromSqlRaw(string query, SqlParameterExpression[] parmeters = null)
        {
            return _repositoryContext.Set<dynamic>().FromSqlRaw(query, parmeters);
        }

        public async Task Rollback(CancellationToken cancellationToken)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
