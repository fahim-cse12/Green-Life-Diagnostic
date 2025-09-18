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
    //[HttpGet(Name = "FinancialReportSearch")]
    //public async Task<IActionResult> FinancialReportSearch(DateTime? startDate, DateTime? endDate, Guid? doctorId)
    //{
    //    var response = await serviceManager.FinancialReportService.FinancialReportSearchByQuery( startDate,endDate, doctorId);

    //    if (response is ApiErrorResponse errorResponse)
    //    {
    //        return BadRequest(new { errorResponse.Message, errorResponse.Errors });
    //    }

    //    return Created("", response);
    //}
    [HttpGet("appointment-wise-income")]
    public async Task<IActionResult> AppointmentWiseIncome(
        DateTime? fromDate,
        DateTime? toDate,
        Guid? doctorId,
        bool? patientType,
        int? patientAge,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var response = await serviceManager.FinancialReportService
            .AppointmentWiseIncomeAsync(fromDate, toDate, doctorId, patientType, patientAge, pageNumber, pageSize);

        if (response is ApiErrorResponse errorResponse)
            return BadRequest(new { errorResponse.Message, errorResponse.Errors });

        return Ok(response);
    }
    [HttpGet("test-wise-income")]
    public async Task<IActionResult> TestWiseIncomeReport(
    DateTime? fromDate,
    DateTime? toDate,
    Guid? investigationId,
    string? patientName,
    int pageNumber = 1,
    int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var response = await serviceManager.FinancialReportService
            .TestWiseIncomeReportAsync(fromDate, toDate, investigationId, patientName, pageNumber, pageSize);

        if (response is ApiErrorResponse errorResponse)
            return BadRequest(new { errorResponse.Message, errorResponse.Errors });

        return Ok(response);
    }

}
