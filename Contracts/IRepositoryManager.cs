using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        Task ExecuteFromSqlRaw(string query, CancellationToken cancellationToken, SqlParameter[] parameters = null);
        IQueryable<T> ExecuteStoredProcedureToGetData<T>(string StoredProcedureName, List<SqlParameter> Parameters = null) where T : class;
        Task ExecuteStoredProcedureAsync(string storedProcedureName, List<SqlParameter> parameters);
        Task BeginTransaction(CancellationToken cancellationToken);
        Task CommitTransaction(CancellationToken cancellationToken);
        Task Rollback(CancellationToken cancellationToken);
    }
}
