using Entities.Responses;
using GreenLife.Presentation.Extentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;

namespace GreenLife.Presentation.Controllers
{
    [Route("api/doctors")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
  //  [Authorize]
    public class DoctorController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public DoctorController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var baseResult = await _service.doctorService.GetAllDoctorAsync(trackChanges: false);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
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

            return Ok(baseResult);
        }

        [HttpPost(Name = "CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] DoctorCreateDto doctorDto)
        {
            var response = await _service.doctorService.CreateDoctorAsync(doctorDto);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteDoctor(Guid id)
        {
            var result = await _service.doctorService.DeleteDoctorAsync(id, trackChanges: false);
            if (result is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }
            return Created("", result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] DoctorCreateDto doctorDto)
        {
            var response = await _service.doctorService.UpdateDoctorAsync(id, doctorDto, trackChanges: true);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }

    }
}
