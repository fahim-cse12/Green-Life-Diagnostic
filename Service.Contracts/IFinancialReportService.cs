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
    Task<ApiBaseResponse> AppointmentWiseIncomeAsync(DateTime? fromDate, DateTime? toDate, Guid? doctorId, bool? patientType, int? patientAge);
    Task<ApiBaseResponse> TestWiseIncomeReportAsync(DateTime? fromDate, DateTime? toDate, Guid? investigationId, string patientName);
}
