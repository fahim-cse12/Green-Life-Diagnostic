using Entities.Responses;
using GreenLife.Presentation.ActionFilter;
using GreenLife.Presentation.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Contracts;
using Shared.DataTransferObject;

namespace GreenLife.Presentation.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class DoctorController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public DoctorController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var baseResult = await _service.doctorService.GetAllDoctorAsync(trackChanges: false);
            var doctors = baseResult.GetResult<IEnumerable<DoctorDto>>();
            return Ok(baseResult);
        }

        [HttpGet("{id:guid}", Name = "DoctorById")]
        public async Task<IActionResult> GetDoctor(Guid id)
        {
            var baseResult = await _service.doctorService.GetDoctorAsync(id, trackChanges: false);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            var doctor = baseResult.GetResult<DoctorDto>();
            return Ok(doctor);
        }

        [HttpPost(Name = "CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorDto doctorDto)
        {
            var response = await _service.doctorService.CreateDoctorAsync(doctorDto);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            var createResponse = (ApiOkResponse<DoctorDto>)response;
            return CreatedAtRoute("DoctorById", new { id = createResponse.Result.Id }, createResponse);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            await _service.doctorService.DeleteDoctorAsync(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] DoctorDto doctorDto)
        {
            await _service.doctorService.UpdateDoctorAsync(id, doctorDto, trackChanges: true);
            return NoContent();
        }

    }
}
