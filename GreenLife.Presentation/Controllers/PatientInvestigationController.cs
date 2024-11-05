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

        [HttpGet("getAllpatientinvestigaion")]
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

        [HttpPut]
        public async Task<IActionResult> UpdatePatientInvestigation([FromBody] PatientInvestigationUpdateDto patientInvestigationUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiErrorResponse("Invalid model", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
            }

            var result = await _service.patientInvestigationService.UpdatePatientInvestigationAsync(patientInvestigationUpdateDto);
            if (result is ApiErrorResponse errorResponse)
            {
                // Return different status codes based on error type
                if (errorResponse.Message == "Not Found")
                    return NotFound(errorResponse);

                if (errorResponse.Message == "Validation failed")
                    return BadRequest(errorResponse);

                return StatusCode(500, errorResponse); // Internal server error for other cases
            }

            // Successful update, return 200 OK with the updated data
            return Ok(result);
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
