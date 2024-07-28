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
    [Route("api/financialRecords")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class FinancialRecordController : ApiControllerBase
    {
        private readonly IServiceManager _service;
        public FinancialRecordController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetFinancialRecords()
        {
            var baseResult = await _service.financialService.GetAllFinanceRecordAsync(trackChanges: false);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }
            return Ok(baseResult);
        }

        [HttpGet("{id:guid}", Name = "getFinancialRecordById")]
        public async Task<IActionResult> GetFinancialRecord(Guid id)
        {
            var baseResult = await _service.financialService.GetFinanceRecordAsync(id, trackChanges: false);
            if (!baseResult.Success)
            {
                return ProcessError(baseResult);
            }

            return Ok(baseResult);
        }

        [HttpPost(Name = "CreateFinancialRecord")]
        public async Task<IActionResult> CreateFinancialRecord([FromBody] FinanceRecordCreateDto FinancialRecordDto)
        {
            var response = await _service.financialService.CreateFinanceRecordAsync(FinancialRecordDto);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteFinancialRecord(Guid id)
        {
            var result = await _service.financialService.DeleteFinanceRecordAsync(id, trackChanges: false);
            if (result is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }
            return Created("", result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateFinancialRecord(Guid id, [FromBody] FinanceRecordCreateDto FinancialRecordDto)
        {
            var response = await _service.financialService.UpdateFinanceRecordAsync(id, FinancialRecordDto, trackChanges: true);

            if (response is ApiErrorResponse errorResponse)
            {
                return BadRequest(new { errorResponse.Message, errorResponse.Errors });
            }

            return Created("", response);
        }
    }
}
