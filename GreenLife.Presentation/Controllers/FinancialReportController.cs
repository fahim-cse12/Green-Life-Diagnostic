using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLife.Presentation.Controllers;
[Route("api/financialReport")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class FinancialReportController (IServiceManager serviceManager) : ApiControllerBase
{
    [HttpGet("appointment-wise-income")]
    public async Task<IActionResult> AppointmentWiseIncome(
        DateOnly? fromDate,
        DateOnly? toDate,
        Guid? doctorId,
        bool? patientType,
        int? patientAge,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var response = await serviceManager.financialReportService
            .AppointmentWiseIncomeAsync(fromDate, toDate, doctorId, patientType, patientAge, pageNumber, pageSize);

        if (response is ApiErrorResponse errorResponse)
            return BadRequest(new { errorResponse.Message, errorResponse.Errors });

        return Ok(response);
    }
    [HttpGet("test-wise-income")]
    public async Task<IActionResult> TestWiseIncomeReport(
    DateOnly? fromDate,
    DateOnly? toDate,
    Guid? investigationId,
    string? patientName,
    int pageNumber = 1,
    int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var response = await serviceManager.financialReportService
            .TestWiseIncomeReportAsync(fromDate, toDate, investigationId, patientName, pageNumber, pageSize);

        if (response is ApiErrorResponse errorResponse)
            return BadRequest(new { errorResponse.Message, errorResponse.Errors });

        return Ok(response);
    }

}
