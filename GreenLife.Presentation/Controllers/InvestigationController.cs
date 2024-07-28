using Entities.Responses;
using GreenLife.Presentation.Extentions;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLife.Presentation.Controllers
{
    [Route("api/investigations")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class InvestigationController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public InvestigationController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetInvestigation()
        {
            var baseResult = await _service.investigationService.GetAllInvestigationAsync(trackChanges: false);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return Ok(baseResult);
        }

        [HttpGet("{id:guid}", Name = "GetInvestigationById")]
        public async Task<IActionResult> GetInvestigation(Guid id)
        {
            var baseResult = await _service.investigationService.GetInvestigationAsync(id, trackChanges: false);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }

            return Ok(baseResult);
        }

        [HttpPost(Name = "CreateInvestigation")]
        public async Task<IActionResult> CreateInvestigation([FromBody] InvestigationCreateDto investigationCreateDto)
        {
            var response = await _service.investigationService.CreateInvestigationAsync(investigationCreateDto);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteInvestigation(Guid id)
        {
            var result = await _service.investigationService.DeleteInvestigationAsync(id, trackChanges: false);
            if (result is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }
            return Created("", result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateInvestigation(Guid id, [FromBody] InvestigationCreateDto investigationdto)
        {
            var response = await _service.investigationService.UpdateInvestigationAsync(id, investigationdto, trackChanges: true);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }
    }
}
