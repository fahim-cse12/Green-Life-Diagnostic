using Dapper;
using Microsoft.Data.SqlClient;
using System.Dynamic;

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
        IQueryable<T> ExecuteStoredProcedureToGetData<T>(string StoredProcedureName, List<SqlParameter> Parameters = null) where T : class;
        Task<string> ExecuteStoreProcedure(string spName, DynamicParameters parameters);
        Task BeginTransaction(CancellationToken cancellationToken);
        Task CommitTransaction(CancellationToken cancellationToken);
        Task Rollback(CancellationToken cancellationToken);
    }
}
