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
    public async Task<ApiBaseResponse> AppointmentWiseIncomeAsync(
        DateOnly? fromDate,
        DateOnly? toDate,
        Guid? doctorId,
        bool? patientType,
        int? patientAge,
        string ticketUniqueId,
        int pageNumber,
        int pageSize)
    {
        string sp = DatabaseProcedure.AppointmentWiseIncomeQuery;

        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@FromDate", fromDate ?? (object)DBNull.Value),
            new SqlParameter("@ToDate", toDate ?? (object)DBNull.Value),
            new SqlParameter("@DoctorId", doctorId ?? (object)DBNull.Value),
            new SqlParameter("@PatientType", patientType.HasValue ? (object)patientType.Value : DBNull.Value),
            new SqlParameter("@PatientAge", patientAge ?? (object)DBNull.Value),
            new SqlParameter("@TicketUniqueId", ticketUniqueId ?? (object)DBNull.Value),
            new SqlParameter("@PageNumber", pageNumber),
            new SqlParameter("@PageSize", pageSize)
        };

        var result = await repository.ExecuteStoredProcedureToGetData<AppointmentWiseIncomeDto>(
            sp, parameters).ToListAsync();
        
        var totalRecords = result.First().TotalRecords;


        return !result.Any() ? new PagedApiResponse<IEnumerable<AppointmentWiseIncomeDto>>([], totalRecords,"Data not found") 
            : new PagedApiResponse<IEnumerable<AppointmentWiseIncomeDto>>(result,totalRecords, "Appointment income data retrieved successfully");

    }


    public async Task<ApiBaseResponse> TestWiseIncomeReportAsync(
        DateOnly? fromDate,
        DateOnly? toDate,
        string? patientInvestigationId,
        Guid? investigationId,
        string? patientName,
        int pageNumber = 1,
        int pageSize = 10)
    {
        string sp = DatabaseProcedure.TestWiseIncomeReportQuery;

        var parameters = new List<SqlParameter>
        {
            new SqlParameter("@FromDate", fromDate ?? (object)DBNull.Value),
            new SqlParameter("@ToDate", toDate ?? (object)DBNull.Value),
            new SqlParameter("@InvestigationId", investigationId ?? (object)DBNull.Value),
            new SqlParameter("@PatientName", patientName ?? (object)DBNull.Value),
            new SqlParameter("@PatientInvestigationUniqueId", patientInvestigationId ?? (object)DBNull.Value),
            new SqlParameter("@PageNumber", pageNumber),
            new SqlParameter("@PageSize", pageSize)
        };

        var result = await repository.ExecuteStoredProcedureToGetData<TestWiseIncomeReportDto>(
            sp, parameters).ToListAsync();
        
        var totalRecords = result.First().TotalRecords;

        return !result.Any() ? new PagedApiResponse<IEnumerable<TestWiseIncomeReportDto>>([], totalRecords, "Data not found")
            : new PagedApiResponse<IEnumerable<TestWiseIncomeReportDto>>(result, totalRecords, "Test income data retrieved successfully");

    }

}
