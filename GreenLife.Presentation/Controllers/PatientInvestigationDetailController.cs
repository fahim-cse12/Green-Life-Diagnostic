using Entities.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace GreenLife.Presentation.Controllers
{
    [Authorize]
    [Route("api/patientinvestigationdetail")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PatientInvestigationDetailController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public PatientInvestigationDetailController(IServiceManager service)
        {
            _service = service;

        }

        [HttpDelete("patient-investigations/{patientInvestigationId:guid}/details/{id:guid}",  Name = "DeletePatientInvestigationDetails")]
        public async Task<IActionResult> DeletePatientInvestigationDetailById(Guid patientInvestigationId, Guid id)
        {
            var result = await _service.patientInvestigationService.DeletePatientInvestigationDetailAsync(patientInvestigationId,id);
            if (result is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }
            return Created("", result);
        }



    }
}
