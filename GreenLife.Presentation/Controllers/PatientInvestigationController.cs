using Entities.Models;
using Entities.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;
using System.Security.Claims;

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


        [HttpPost(Name = "patientinvestigation")]
        public async Task<IActionResult> CreatePatientInvestigation([FromBody] List<PatientInvestigationCreateDto> investigationCreateDtos)
        {
            var response = await _service.patientInvestigationService.CreatePatientInvestigationAsync(investigationCreateDtos);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }
    }
}
