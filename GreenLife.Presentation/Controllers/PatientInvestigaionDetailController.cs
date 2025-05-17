using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace GreenLife.Presentation.Controllers
{
    [Route("api/patientinvestigationdetail")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PatientInvestigaionDetailController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public PatientInvestigaionDetailController(IServiceManager service)
        {
            _service = service;

        }

        [HttpDelete("{id:guid}", Name = "DeletePatientInvestigationDetails")]
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
