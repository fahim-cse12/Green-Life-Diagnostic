using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;

namespace GreenLife.Presentation.Controllers
{
    [Route("api/patientinvestigation")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PatientInvestigationController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public PatientInvestigationController(IServiceManager service)
        {
            _service = service;

        }


        [HttpPost("investigaioncreate", Name = "CreatePatientInvestigation")]
        public async Task<IActionResult> CreatePatientInvestigation([FromBody] PatientInvestigationCreateDto investigationCreateDtos)
        {
            var response = await _service.patientInvestigationService.CreatePatientInvestigationAsync(investigationCreateDtos);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredPatientInvestigations(
       [FromQuery] string? patientInvestigationUniqueId,
       [FromQuery] string? patientUniqueId,
       [FromQuery] string? patientName,
       [FromQuery] string? patientMobileNo,
       [FromQuery] DateTime? fromDate,
       [FromQuery] DateTime? toDate)
        {
            // Call service method and pass the query parameters
            var response = await _service.patientInvestigationService.GetFilteredPatientInvestigationsAsync(
                patientInvestigationUniqueId, patientUniqueId, patientName, patientMobileNo, fromDate, toDate, 1, 10, false);

            // If it's an error response, return appropriate error status
            if (response is ApiErrorResponse errorResponse)
            {
                return StatusCode(500, errorResponse);
            }

            return Ok(response);
        }

        //[HttpPost("investigaionresultcreate", Name = "CreateInvestigationResult")]
        //public async Task<IActionResult> CreateInvestigationResult([FromBody] List<InvestigationResultCreateDto> investigationResultCreateDtos)
        //{
        //    var response = await _service.patientInvestigationService.CreateInvestigationResultAsync(investigationResultCreateDtos);

        //    if (response is ApiErrorResponse errorResponse)
        //    {
        //        return BadRequest(new { errorResponse.Message, errorResponse.Errors });
        //    }

        //    return Created("", response);
        //}
    }
}
