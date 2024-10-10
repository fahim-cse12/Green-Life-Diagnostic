using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using FluentValidation;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Service.Contracts;
using Shared.DataTransferObject;

namespace Service
{
    internal sealed class PatientInvestigationService : IPatientInvestigationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
      //  private readonly IValidator<InvestigationResult> _investigationResultValidator;
        private readonly IValidator<PatientInvestigation> _patientInvestigationValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PatientInvestigationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
         //  IValidator<InvestigationResult> investigationResultValidator,
           IValidator<PatientInvestigation> patientInvestigationValidator,IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
          //  _investigationResultValidator = investigationResultValidator;
            _patientInvestigationValidator = patientInvestigationValidator;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<ApiBaseResponse> CreateInvestigationResultAsync(List<InvestigationResultCreateDto> investigationResultListDto)
        //{
        //    try
        //    {
        //        var errorMessages = new List<string>();
        //        List<InvestigationResult> listData = new List<InvestigationResult>();
        //        if (investigationResultListDto.Any())
        //        {
        //            foreach (var item in investigationResultListDto)
        //            {
        //                var investigationResult = _mapper.Map<InvestigationResult>(item);
        //                var validationForInvestigaionResult = await _investigationResultValidator.ValidateAsync(investigationResult);

        //                if (!validationForInvestigaionResult.IsValid)
        //                {
        //                    errorMessages.AddRange(validationForInvestigaionResult.Errors.Select(e => e.ErrorMessage));
        //                }

        //                if (errorMessages.Any())
        //                {
        //                    return new ApiErrorResponse("Validation failed", errorMessages);
        //                }

        //                listData.Add(investigationResult);
        //            }
                   
        //            _repository.InvestigationResultRepository.CreateInvestigationResult(listData);

        //            await _repository.SaveAsync();
                   
        //            return new ApiOkResponse<string>(listData[0].ResultDate.ToString(), $"Investigation Result Created Successfully");
        //        }

        //        return new ApiOkResponse<string>(null, $"No investigation results provided.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiErrorResponse("Something Went Wrong", ex.Message);
        //    }
        //}

        //public async Task<ApiBaseResponse> CreatePatientInvestigationAsync(List<PatientInvestigationCreateDto> investigationListDto)
        //{
        //    try
        //    {
        //        var errorMessages = new List<string>();
        //        List<PatientInvestigation> listData = new List<PatientInvestigation>();
        //        if (investigationListDto.Any())
        //        {          
        //            string uniqueId = $"{"INV"}{DateTime.Now.ToString("ddMMyy")}{DateTime.Now.ToString("ss")}";
        //            foreach (var item in investigationListDto)
        //            {
        //                var patientInvestigation = _mapper.Map<PatientInvestigation>(item);
        //                var validationResultForPatientInvestigaion = await _patientInvestigationValidator.ValidateAsync(patientInvestigation);

        //                if (!validationResultForPatientInvestigaion.IsValid)
        //                {
        //                    errorMessages.AddRange(validationResultForPatientInvestigaion.Errors.Select(e => e.ErrorMessage));
        //                }
        //                patientInvestigation.InvestigationUniqueId = uniqueId;
        //                patientInvestigation.Status = true;
        //                patientInvestigation.CreatedAt = DateTime.Now;
        //                //patientInvestigation.CreatedBy = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //                patientInvestigation.CreatedBy = null;
        //                listData.Add(patientInvestigation);
        //            }
        //            if (errorMessages.Any())
        //            {
        //                return new ApiErrorResponse("Validation failed", errorMessages);
        //            }
        //            _repository.PatientInvestigation.CreatePatientInvestigationList(listData);

        //            await _repository.SaveAsync();

        //            //if ()
        //            //{

        //            //}
        //            //var financialRecord = new FinancialRecord
        //            //{
        //            //    Income = 
        //            //};


        //            return new ApiOkResponse<string>(listData[0].InvestigationUniqueId, $"Investigation Created Successfully");
        //        }

        //        return new ApiOkResponse<string>(null, $"No Investigation provided.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiErrorResponse("Something Went Wrong", ex.Message);
        //    }
        //}

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