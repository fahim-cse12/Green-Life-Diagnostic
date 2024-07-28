using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Entities.Responses;
using FluentValidation;
using LoggerService;
using Service.Contracts;
using Shared.DataTransferObject;

namespace Service
{
    internal sealed class FinancialRecordService : IFinancialService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<FinancialRecord> _validator;
        public FinancialRecordService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IValidator<FinancialRecord> validator)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiBaseResponse> CreateFinanceRecordAsync(FinanceRecordCreateDto financeRecordDto)
        {
            var financialRecordEntity = _mapper.Map<FinancialRecord>(financeRecordDto);
            var validationResult = await _validator.ValidateAsync(financialRecordEntity);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiErrorResponse("Validation failed", errorMessages);
            }
            financialRecordEntity.Status = true;
            financialRecordEntity.CreatedAt = DateTime.Now;
            _repository.FinancialRecord.CreateFinancialRecord(financialRecordEntity);
            await _repository.SaveAsync();

            var FinancialRecordToReturn = _mapper.Map<FinancialRecordDto>(financialRecordEntity);
            return new ApiOkResponse<FinancialRecordDto>(FinancialRecordToReturn, "FinancialRecord created successfully");
        }

        public async Task<ApiBaseResponse> DeleteFinanceRecordAsync(Guid financeRecordId, bool trackChanges)
        {
            var financialRecord = await _repository.FinancialRecord.GetFinancialRecordAsync(financeRecordId, trackChanges);
            if (financialRecord is null)
                return new IdNotFoundResponse<FinancialRecord>(financeRecordId);
            financialRecord.Status = false;
            financialRecord.UpdatedAt = DateTime.Now;
            _repository.FinancialRecord.UpdateFinancialRecord(financialRecord);
            await _repository.SaveAsync();
            return new ApiOkResponse<Guid>(financeRecordId, "FinancialRecord Deleted Successfully");
        }

        public async Task<ApiBaseResponse> GetAllFinanceRecordAsync(bool trackChanges)
        {
            var FinancialRecordList = await _repository.FinancialRecord.GetAllFinancialRecordAsync(trackChanges);

            var FinancialRecordsDto = _mapper.Map<IEnumerable<FinancialRecordDto>>(FinancialRecordList);

            return new ApiOkResponse<IEnumerable<FinancialRecordDto>>(FinancialRecordsDto, "");
        }

        public async Task<ApiBaseResponse> GetFinanceRecordAsync(Guid financeRecordId, bool trackChanges)
        {
            var financialRecord = await _repository.FinancialRecord.GetFinancialRecordAsync(financeRecordId, trackChanges);
            if (financialRecord is null)
                return new IdNotFoundResponse<FinancialRecord>(financeRecordId);
            var FinancialRecordDto = _mapper.Map<FinancialRecordDto>(financialRecord);
            return new ApiOkResponse<FinancialRecordDto>(FinancialRecordDto, "");
        }

        public async Task<ApiBaseResponse> UpdateFinanceRecordAsync(Guid financeRecordId, FinanceRecordCreateDto financeRecordDto, bool trackChanges)
        {
            var financialRecordEntity = _mapper.Map<FinancialRecord>(financeRecordDto);
            var validationResult = await _validator.ValidateAsync(financialRecordEntity);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiErrorResponse("Validation failed", errorMessages);
            }

            var financialRecord = await _repository.FinancialRecord.GetFinancialRecordAsync(financeRecordId, trackChanges);
            if (financialRecord is null)
                throw new IdNotFoundException<FinancialRecord>(financeRecordId);
            financialRecord.UpdatedAt = DateTime.Now;
            _mapper.Map(financeRecordDto, financialRecord);
            await _repository.SaveAsync();
            return new ApiOkResponse<Guid>(financeRecordId, "FinancialRecord Updated Successfully");
        }
    }
}
