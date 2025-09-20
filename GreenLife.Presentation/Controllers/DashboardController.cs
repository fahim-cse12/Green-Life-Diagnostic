using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace GreenLife.Presentation.Controllers;
[Route("api/dashboard")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class DashboardController(IServiceManager serviceManager) : ApiControllerBase
{
    [HttpGet("data")]
    public async Task<IActionResult> GetDashboardData(
        DateTime? startDate, DateTime? endDate)
    {
        var response = await serviceManager.dashboardService.GetDashboardDataAsync(startDate, endDate);

        if (response is ApiErrorResponse errorResponse)
            return BadRequest(new { errorResponse.Message, errorResponse.Errors });

        return Ok(response);
    }


}

