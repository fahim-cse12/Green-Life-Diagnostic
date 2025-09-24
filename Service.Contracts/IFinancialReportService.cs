using Entities.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;
public interface IFinancialReportService
{
    //Task<ApiBaseResponse> FinancialReportSearchByQuery(DateTime? startDate, DateTime? endDate, Guid? doctorId);
    Task<ApiBaseResponse> AppointmentWiseIncomeAsync(DateOnly? fromDate, DateOnly? toDate, Guid? doctorId, bool? patientType, int? patientAge, int pageNumber, int pageSize);
    Task<ApiBaseResponse> TestWiseIncomeReportAsync(DateOnly? fromDate, DateOnly? toDate, Guid? investigationId, string? patientName, int pageNumber, int pageSize);
}
