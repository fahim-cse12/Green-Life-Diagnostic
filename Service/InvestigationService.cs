using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Entities.Responses;
using FluentValidation;
using LoggerService;
using Service.Contracts;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class InvestigationService : IInvestigationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<Investigation> _validator;
        public InvestigationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IValidator<Investigation> validator)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ApiBaseResponse> CreateInvestigationAsync(InvestigationCreateDto investigationDto)
        {
            var investigationEntity = _mapper.Map<Investigation>(investigationDto);
            var validationResult = await _validator.ValidateAsync(investigationEntity);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();  
                return new ApiErrorResponse("Validation failed", errorMessages);
            }
            investigationEntity.Status = true;
            investigationEntity.CreatedAt = DateTime.Now;
            _repository.Investigation.CreateInvestigation(investigationEntity);
            await _repository.SaveAsync();

            var investigationToReturn = _mapper.Map<InvestigationDto>(investigationEntity);
            return new ApiOkResponse<InvestigationDto>(investigationToReturn, "Investigation created successfully");
        }

        public async Task<ApiBaseResponse> DeleteInvestigationAsync(Guid investigationId, bool trackChanges)
        {
            var investigation = await _repository.Investigation.GetInvestigationAsync(investigationId, trackChanges);
            if (investigation is null)
                return new IdNotFoundResponse<Investigation>(investigationId);
            investigation.Status = false;
            investigation.UpdatedAt = DateTime.Now;
            _repository.Investigation.UpdateInvestigation(investigation);
            await _repository.SaveAsync();
            return new ApiOkResponse<Guid>(investigationId, "Investigation Deleted Successfully");
        }

        public async Task<ApiBaseResponse> GetAllInvestigationAsync(bool trackChanges)
        {
            var investigationList = await _repository.Investigation.GetAllInvestigationAsync(trackChanges);

            var investigationDtos = _mapper.Map<IEnumerable<InvestigationDto>>(investigationList);

            return new ApiOkResponse<IEnumerable<InvestigationDto>>(investigationDtos, "");
        }

        public async Task<ApiBaseResponse> GetInvestigationAsync(Guid investigationId, bool trackChanges)
        {
            var investigation = await _repository.Investigation.GetInvestigationAsync(investigationId, trackChanges);
            if (investigation is null)
                return new IdNotFoundResponse<Investigation>(investigationId);
            var investigationDto = _mapper.Map<InvestigationDto>(investigation);
            return new ApiOkResponse<InvestigationDto>(investigationDto, "");
        }

        public async Task<ApiBaseResponse> UpdateInvestigationAsync(Guid investigationId, InvestigationCreateDto investigationDto, bool trackChanges)
        {
            var investigationEntity = _mapper.Map<Investigation>(investigationDto);
            var validationResult = await _validator.ValidateAsync(investigationEntity);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiErrorResponse("Validation failed", errorMessages);
            }

            var investigation = await _repository.Investigation.GetInvestigationAsync(investigationId, trackChanges);
            if (investigation is null)
                throw new IdNotFoundException<Investigation>(investigationId);
            investigation.UpdatedAt = DateTime.Now;
            _mapper.Map(investigationDto, investigation);
            await _repository.SaveAsync();
            return new ApiOkResponse<Guid>(investigationId, "Investigation Updated Successfully");
        }
    }
}
