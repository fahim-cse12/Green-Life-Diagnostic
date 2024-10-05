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

        [HttpPost(Name = "purchaseticket")]
        public async Task<IActionResult> CreatePatientByTicket([FromBody] PurchageTicketDto purchageTicketDto)
        {
            var response = await _service.patientService.PurchageTicketAsync(purchageTicketDto);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }

        [HttpGet(Name = "patientsearch")]
        public async Task<IActionResult> PatientSearchByQeuery(string ticketId = null, string patientName = null, string mobileNo = null, string doctorName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var response = await _service.patientService.PatientSearchByQuery(ticketId, patientName, mobileNo, doctorName, startDate, endDate);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var result = await _service.patientService.DeletePurchasedTicketAsync(id, trackChanges: false);
            if (result is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }
            return Created("", result);
        }
    }
}
