using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities.NonDbEntities;
using Entities.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObject;
using Shared.Utility;

namespace Service;
public class FinancialReportService(IRepositoryManager repository) : IFinancialReportService
{
    //public async Task<ApiBaseResponse> FinancialReportSearchByQuery(DateTime? startDate, DateTime? endDate, Guid? doctorId)
    //{
    //    string sp = DatabaseProcedure.FinancialReportQuery;
    //    var parameters = new List<SqlParameter>
    //    {
    //        new SqlParameter("@StartDate", startDate.HasValue ? (object)startDate.Value : DBNull.Value),
    //        new SqlParameter("@EndDate", endDate.HasValue ? (object)endDate.Value : DBNull.Value),
    //        new SqlParameter("@DoctorId", doctorId.HasValue ? (object)doctorId.Value : DBNull.Value)
    //    };

    //    var result = await repository.ExecuteStoredProcedureToGetData<FinancialReportDto>(sp, parameters).ToListAsync();

    //    return !result.Any() ? new ApiOkResponse<IEnumerable<FinancialReportDto>>([], "Data not found") : new ApiOkResponse<IEnumerable<FinancialReportDto>>(result, "Patients retrieved successfully");
    //}
    public async Task<ApiBaseResponse> AppointmentWiseIncomeAsync(DateTime? fromDate, DateTime? toDate, Guid? doctorId, bool? patientType, int? patientAge)
    {
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@FromDate", fromDate ?? (object)DBNull.Value),
            new SqlParameter("@ToDate", toDate ?? (object)DBNull.Value),
            new SqlParameter("@DoctorId", doctorId ?? (object)DBNull.Value),
            new SqlParameter("@PatientType", patientType.HasValue ? (object)patientType.Value : DBNull.Value),
            new SqlParameter("@PatientAge", patientAge ?? (object)DBNull.Value)
        };

        var result = await repository.ExecuteStoredProcedureToGetData<AppointmentWiseIncomeDto>("Sp_AppointmentWiseIncome", parameters).ToListAsync();

        return !result.Any()
            ? new ApiOkResponse<IEnumerable<AppointmentWiseIncomeDto>>([], "No appointment income data found")
            : new ApiOkResponse<IEnumerable<AppointmentWiseIncomeDto>>(result, "Appointment income data retrieved successfully");
    }

    public async Task<ApiBaseResponse> TestWiseIncomeReportAsync(DateTime? fromDate, DateTime? toDate, Guid? investigationId, string? patientName)
    {
        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@FromDate", fromDate ?? (object)DBNull.Value),
            new SqlParameter("@ToDate", toDate ?? (object)DBNull.Value),
            new SqlParameter("@InvestigationId", investigationId ?? (object)DBNull.Value),
            new SqlParameter("@PatientName", patientName ?? (object)DBNull.Value)
        };

        var result = await repository.ExecuteStoredProcedureToGetData<TestWiseIncomeReportDto>("Sp_TestWiseIncomeReport", parameters).ToListAsync();

        return !result.Any()
            ? new ApiOkResponse<IEnumerable<TestWiseIncomeReportDto>>([], "No test income data found")
            : new ApiOkResponse<IEnumerable<TestWiseIncomeReportDto>>(result, "Test income data retrieved successfully");
    }
}
