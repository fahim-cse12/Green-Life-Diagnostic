using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLife.Presentation.Controllers;
[Authorize]
[Route("api/dashboard")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
public class DashboardController(IServiceManager serviceManager) : ApiControllerBase
{
    [HttpGet("data")]
    public async Task<IActionResult> GetDashboardData(
        DateOnly? startDate, DateOnly? endDate)
    {
        var response = await serviceManager.dashboardService.GetDashboardDataAsync(startDate, endDate);

        if (response is ApiErrorResponse errorResponse)
            return BadRequest(new { errorResponse.Message, errorResponse.Errors });

        return Ok(response);
    }


}

