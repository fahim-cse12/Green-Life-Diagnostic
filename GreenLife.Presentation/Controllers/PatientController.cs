using Entities.Responses;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;

namespace GreenLife.Presentation.Controllers
{
    [Route("api/patient")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class PatientController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public PatientController(IServiceManager service ) 
        {
            _service = service;
        }

        [HttpPost(Name = "PuchageTicket")]
        public async Task<IActionResult> CreatePatientByTicket([FromBody] PurchageTicketDto purchageTicketDto)
        {
            var response = await _service.patientService.PurchageTicketAsync(purchageTicketDto);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }
    }
}
