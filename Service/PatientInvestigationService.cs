using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using Entities.Validators;
using FluentValidation;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class PatientInvestigationService : IPatientInvestigationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<PatientInvestigation> _patientInvestigationValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PatientInvestigationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
           IValidator<PatientInvestigation> patientInvestigationValidator, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _patientInvestigationValidator = patientInvestigationValidator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiBaseResponse> CreatePatientInvestigationAsync(List<PatientInvestigationCreateDto> investigationListDto)
        {
            try
            {
                var errorMessages = new List<string>();
                List<PatientInvestigation> listData = new List<PatientInvestigation>();
                if (investigationListDto.Any())
                {                   
                    foreach (var item in investigationListDto)
                    {
                        var patientInvestigation = _mapper.Map<PatientInvestigation>(item);
                        var validationResultForPatientInvestigaion = await _patientInvestigationValidator.ValidateAsync(patientInvestigation);

                        if (!validationResultForPatientInvestigaion.IsValid)
                        {
                            errorMessages.AddRange(validationResultForPatientInvestigaion.Errors.Select(e => e.ErrorMessage));
                        }
                        patientInvestigation.InvestigationUniqueId = $"{"INV"}{DateTime.Now.ToString("ddMMyy")}{DateTime.Now.ToString("ss")}";
                        patientInvestigation.Status = true;
                        patientInvestigation.CreatedAt = DateTime.Now;
                        //patientInvestigation.CreatedBy = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                        patientInvestigation.CreatedBy = null;
                        listData.Add(patientInvestigation);
                    }
                    if (errorMessages.Any())
                    {
                        return new ApiErrorResponse("Validation failed", errorMessages);
                    }
                    _repository.PatientInvestigation.CreatePatientInvestigationList(listData);

                    await _repository.SaveAsync();
                   
                }

                return new ApiOkResponse<string>(listData[0].InvestigationUniqueId, $"Investigation Created Successfully");
            }
            catch (Exception ex)
            {
                return new ApiErrorResponse("Something Went Wrong", ex.Message);
            }
        }

        public async Task<ApiBaseResponse> GetAllPtientInvestigationAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }
    }
}

//[
//  {
//    "patientId": "14e239c2-8cfe-48ff-d1f1-08dcb0acb85a",
//    "doctorId": "20326B15-F84D-493D-D1A6-08DCB0AA64F2",
//    "investigationId": "DED98BF0-5B86-4EC9-9622-08DCE56309BC",
//    "investigationUniqueId": "string",
//    "payAmount": 450,
//    "dueAmount": 0,
//    "discountAmount": 0,
//    "deliveryDate": "",
//    "isDelivered": false
//  }
//]