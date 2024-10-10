using Contracts;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Text;

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
        //private readonly Lazy<IInvestigationResultRepository> _investigationResultRepository;
        private IDbContextTransaction _transaction;
        private readonly IDbConnection _dbConnection;


        public RepositoryManager(RepositoryContext repositoryContext, IDbConnection dbConnection)
        {
            _repositoryContext = repositoryContext;
            _doctorRepository = new Lazy<IDoctorRepository>(() => new DoctorRepository(repositoryContext));
            _patientRepostory = new Lazy<IPatientRepository>(() => new PatientRepository(repositoryContext));
            _patientInvestigationRepository = new Lazy<IPatientInvestigationRepository>(() => new PatientInvestigationRepository(repositoryContext));
            _ticketRepository = new Lazy<ITicketRepository>(() => new TicketRepository(repositoryContext));
            _investigationRepository = new Lazy<IInvestigationRepository>(() => new InvestigationRepository(repositoryContext));
            _financialRepository = new Lazy<IFinancialRepository>(() => new FinancialRecordRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
         //   _investigationResultRepository = new Lazy<IInvestigationResultRepository>(() => new InvestigationResultRepository(repositoryContext));
            _dbConnection = dbConnection;
        }

        public IDoctorRepository Doctor => _doctorRepository.Value;

        public IPatientRepository Patient => _patientRepostory.Value;

        public IPatientInvestigationRepository PatientInvestigation => _patientInvestigationRepository.Value;

        public ITicketRepository Ticket => _ticketRepository.Value;

        public IInvestigationRepository Investigation => _investigationRepository.Value;

        public IFinancialRepository FinancialRecord => _financialRepository.Value;

        public IUserRepository User => _userRepository.Value;
      //  public IInvestigationResultRepository InvestigationResultRepository => _investigationResultRepository.Value;

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

        public async Task ExecuteFromSqlRaw(string query, CancellationToken cancellationToken, SqlParameter[] parameters = null)
        {
            await _repositoryContext.Database.ExecuteSqlRawAsync(query, cancellationToken, parameters);
        }

        public IQueryable<T> ExecuteStoredProcedureToGetData<T>(string StoredProcedureName, List<SqlParameter> Parameters = null) where T : class
        {
            if (Parameters != null && Parameters.Any())
            {
                string parameterNames = string.Join(", ", Parameters.Select(x => x.ParameterName).ToArray());

                return _repositoryContext.Set<T>().FromSqlRaw(string.Format("EXEC {0} {1}", StoredProcedureName, parameterNames), Parameters.ToArray());
            }
            else
            {
                return _repositoryContext.Set<T>().FromSqlRaw(string.Format("EXEC {0}", StoredProcedureName));
            }
        }

        public async Task ExecuteStoredProcedureAsync(string storedProcedureName, List<SqlParameter> parameters)
        {
            var commandText = new StringBuilder();
            commandText.Append($"EXEC {storedProcedureName} ");

            if (parameters != null && parameters.Any())
            {
                commandText.Append(string.Join(", ", parameters.Select(p => p.ParameterName)));
            }

            await _repositoryContext.Database.ExecuteSqlRawAsync(commandText.ToString(), parameters.ToArray());
        }

        public async Task<string> ExecuteSqlRawAsync(string storedProcedure, SqlParameter[] parameters)
        {
            var outputParameter = new SqlParameter
            {
                ParameterName = "@ResponseMessage",
                SqlDbType = SqlDbType.NVarChar,
                Size = 4000,
                Direction = ParameterDirection.Output
            };
            parameters = parameters.Append(outputParameter).ToArray();

            var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName + " = @" + p.ParameterName.TrimStart('@')));

            var commandText = $"EXEC {storedProcedure} {parameterNames}";

            await _repositoryContext.Database.ExecuteSqlRawAsync(commandText, parameters);

            var responseMessage = outputParameter.Value != DBNull.Value ? (string)outputParameter.Value : null;
            Console.WriteLine($"Stored Procedure Response: {responseMessage}");

            return responseMessage;
        }

        public async Task Rollback(CancellationToken cancellationToken)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

        public async Task<string> ExecuteStoreProcedure(string spName, DynamicParameters parameters)
        {
            var results = await _dbConnection.QueryAsync<string>(spName, parameters, commandType: CommandType.StoredProcedure);
            return results.FirstOrDefault();
        }

    }
}
