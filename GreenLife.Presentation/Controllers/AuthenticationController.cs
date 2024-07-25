using Entities.Responses;
using GreenLife.Presentation.ActionFilter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;

namespace GreenLife.Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthenticationController(IServiceManager service) => _service = service;

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.authenticationService.RegisterUser(userForRegistration);
            if (result is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }
            var createResponse = (ApiOkResponse<UserForRegistrationDto>)result;

            return Created("User Created", createResponse);
        }
    }
}
