using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using FluentValidation;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObject;
using System.Threading;

namespace Service
{
    internal sealed class PatientInvestigationService : IPatientInvestigationService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<PatientInvestigationDetail> _patientInvestigationDetailValidator;
        private readonly IValidator<PatientInvestigation> _patientInvestigationValidator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PatientInvestigationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
           IValidator<PatientInvestigationDetail> patientInvestigationDetailValidator,
           IValidator<PatientInvestigation> patientInvestigationValidator,IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _patientInvestigationDetailValidator = patientInvestigationDetailValidator;
            _patientInvestigationValidator = patientInvestigationValidator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiBaseResponse> CreatePatientInvestigationAsync(PatientInvestigationCreateDto patientInvestigationCreateDto)
        {
            CancellationToken cancellationToken = default;
            var currentDate = DateTime.Now;

            if (!patientInvestigationCreateDto.PatientInvestigationDetailCreateDtos.Any())
            {
                return new ApiErrorResponse("Validation failed", new List<string> { "At least one investigation should be added" });
            }

            // Map PatientInvestigation from DTO
            var patientInvestigation = _mapper.Map<PatientInvestigation>(patientInvestigationCreateDto);
            patientInvestigation.PatientInvestigationUniqueId = $"INV{DateTime.Now:ddMMyyHHmmss}";
            patientInvestigation.CreatedAt = currentDate;
            patientInvestigation.UpdatedAt = currentDate;
            patientInvestigation.Status = true;
            patientInvestigation.IsDelivered = false;

            // Map Investigation Details
            var detailList = _mapper.Map<List<PatientInvestigationDetail>>(patientInvestigationCreateDto.PatientInvestigationDetailCreateDtos);
            patientInvestigation.InvestigationDetails = detailList;

            // Calculate financials
            patientInvestigation.CalculateFinancials(patientInvestigation.DiscountAmount);
            patientInvestigation.UpdateDueAmount();
            patientInvestigation.InvestigationDetails = null;
            // Validate PatientInvestigation
            var investigationValidationResult = await _patientInvestigationValidator.ValidateAsync(patientInvestigation);
            if (!investigationValidationResult.IsValid)
            {
                return new ApiErrorResponse("Validation failed", investigationValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            await _repository.BeginTransaction(cancellationToken);

            try
            {
                // Save PatientInvestigation
                _repository.PatientInvestigation.CreatePatientInvestigation(patientInvestigation);
                await _repository.SaveAsync(); // This will generate PatientInvestigationId

                // Update details with generated PatientInvestigationId and common fields
                foreach (var detail in detailList)
                {
                    detail.PatientInvestigationId = patientInvestigation.PatientInvestigationId;
                    detail.CreatedAt = currentDate;
                    detail.UpdatedAt = currentDate;
                    detail.IsDelivered = false;
                    detail.IsActive = true;

                    // Validate each detail
                    var detailValidationResult = await _patientInvestigationDetailValidator.ValidateAsync(detail);
                    if (!detailValidationResult.IsValid)
                    {
                        return new ApiErrorResponse("Validation failed", detailValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
                    }
                }

                // Save all details in batch
                _repository.InvestigationDetailsRepository.CreatePatientInvestigationDetails(detailList);
                await _repository.SaveAsync();

                // Commit transaction
                await _repository.CommitTransaction(cancellationToken);

                // Map to DTO
                var patientInvestigationDto = _mapper.Map<PatientInvestigationDto>(patientInvestigation);
                return new ApiOkResponse<PatientInvestigationDto>(patientInvestigationDto, "Patient Investigation Created Successfully");
            }
            catch (Exception ex)
            {
                await _repository.Rollback(cancellationToken);
                return new ApiErrorResponse("Something Went Wrong", ex.Message);
            }
        }

        public async Task<ApiBaseResponse> DeletePatientInvestigationDetailAsync(Guid detailId)
        {
            var result = await _repository.InvestigationDetailsRepository.GetPatientInvestigationDetailById(detailId, false);
            if (result == null)
            {
                return new ApiErrorResponse("Not Found", new List<string> { "Patient Investigation not found" });
            }
            result.IsActive = false;
            result.UpdatedAt = DateTime.Now;
            _repository.InvestigationDetailsRepository.UpdateSinglePatientInvestigationDetails(result);
            await _repository.SaveAsync();

            return new ApiOkResponse<Guid>(detailId, "Patient Investigation Detail Deleted Successfully");
        }

        public async Task<ApiBaseResponse> GetFilteredPatientInvestigationsAsync(string? patientInvestigationUniqueId, string? patientUniqueId, 
            string? patientName, string? patientMobileNo, DateTime? fromDate, DateTime? toDate, int pageNumber, int pageSize, bool trackChanges)
        {
            try
            {
                // Build the query from PatientInvestigations table
                var query = _repository.PatientInvestigation.GetAllInvestigations(trackChanges);

                // Apply filters if parameters are provided
                if (!string.IsNullOrWhiteSpace(patientInvestigationUniqueId))
                {
                    query = query.Where(pi => pi.PatientInvestigationUniqueId.Contains(patientInvestigationUniqueId));
                }

                if (!string.IsNullOrWhiteSpace(patientUniqueId))
                {
                    query = query.Where(pi => pi.PatientUniqueId.Contains(patientUniqueId));
                }

                if (!string.IsNullOrWhiteSpace(patientName))
                {
                    query = query.Where(pi => pi.PatientName.Contains(patientName));
                }

                if (!string.IsNullOrWhiteSpace(patientMobileNo))
                {
                    query = query.Where(pi => pi.PatientMobileNo.Contains(patientMobileNo));
                }

                if (fromDate.HasValue)
                {
                    query = query.Where(pi => pi.CreatedAt >= fromDate.Value);
                }

                if (toDate.HasValue)
                {
                    query = query.Where(pi => pi.CreatedAt <= toDate.Value);
                }

                // Get total record count for pagination
                var totalRecords = await query.CountAsync();

                // Apply pagination using Skip and Take
                var patientInvestigations = await query
                    .Include(pi => pi.InvestigationDetails)
                    .ThenInclude(detail => detail.Investigation)
                    .Skip((pageNumber - 1) * pageSize) // Skip the previous pages
                    .Take(pageSize) // Take only the page size number of records
                    .ToListAsync();

                // Map the result to DTO
                var patientInvestigationDtos = _mapper.Map<List<PatientInvestigationDto>>(patientInvestigations);

                // Create a paged response
                var pagedResponse = new PagedResponse<List<PatientInvestigationDto>>(
                    patientInvestigationDtos,
                    pageNumber,
                    pageSize,
                    totalRecords
                );

                return new ApiOkResponse<PagedResponse<List<PatientInvestigationDto>>>(pagedResponse);
            }
            catch (Exception ex)
            {
                return new ApiErrorResponse("An error occurred while retrieving patient investigations", ex.Message);
            }
        }

        public async Task<ApiBaseResponse> UpdatePatientInvestigationAsync(PatientInvestigationDto patientInvestigationDto)
        {
            CancellationToken cancellationToken = default;
            var currentDate = DateTime.Now;

            if (!patientInvestigationDto.InvestigationDetails.Any())
            {
                return new ApiErrorResponse("Validation failed", new List<string> { "At least one investigation should be added" });
            }

            // Fetch existing patient investigation
            var patientInvestigation = await _repository.PatientInvestigation.GetPatientInvestigationById(patientInvestigationDto.PatientInvestigationId, false);
            if (patientInvestigation == null)
            {
                return new ApiErrorResponse("Not Found", new List<string> { "Patient Investigation not found" });
            }

            // Map the updated PatientInvestigation from DTO
            _mapper.Map(patientInvestigationDto, patientInvestigation);
            patientInvestigation.UpdatedAt = currentDate;

            // Map Investigation Details (add or update based on the PatientInvestigationId)
            var detailList = _mapper.Map<List<PatientInvestigationDetail>>(patientInvestigationDto.InvestigationDetails);

            // To handle changes in investigation details, we need to decide if the existing details should be updated or removed
            var existingDetails = await _repository.InvestigationDetailsRepository.GetInvestigationDetailByPatientInvestigationId(patientInvestigation.PatientInvestigationId, false);

            foreach (var detail in existingDetails)
            {
                // Check if each existing detail is still in the updated list, otherwise mark for deletion or change status
                var updatedDetail = detailList.FirstOrDefault(d => d.InvestigationId == detail.InvestigationId);
                if (updatedDetail != null)
                {
                    // Update existing detail
                    _mapper.Map(updatedDetail, detail);
                }
                else
                {
                   var isDelete = await DeletePatientInvestigationDetail(updatedDetail.PatientInvestigationDetailId);
                    if(isDelete)
                        return new IdNotFoundResponse<PatientInvestigationDetail>(updatedDetail.PatientInvestigationDetailId);
                }
            }

            //// Add new details (if any)
            foreach (var newDetail in detailList.Where(d => !existingDetails.Any(ed => ed.InvestigationId == d.InvestigationId)))
            {
                newDetail.PatientInvestigationId = patientInvestigation.PatientInvestigationId;
                newDetail.CreatedAt = currentDate;
                newDetail.UpdatedAt = currentDate;
                existingDetails.Add(newDetail);
            }

            // Recalculate financials based on the updated details
            patientInvestigation.CalculateFinancials(patientInvestigation.DiscountAmount);
            patientInvestigation.UpdateDueAmount();

            // Validate PatientInvestigation and Details
            var investigationValidationResult = await _patientInvestigationValidator.ValidateAsync(patientInvestigation);
            if (!investigationValidationResult.IsValid)
            {
                return new ApiErrorResponse("Validation failed", investigationValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }

            foreach (var detail in detailList)
            {
                var detailValidationResult = await _patientInvestigationDetailValidator.ValidateAsync(detail);
                if (!detailValidationResult.IsValid)
                {
                    return new ApiErrorResponse("Validation failed", detailValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
                }
            }

            // Save changes in a transaction
            await _repository.BeginTransaction(cancellationToken);

            try
            {
                // Update PatientInvestigation in the repository
                _repository.PatientInvestigation.UpdatePatientInvestigation(patientInvestigation);
                await _repository.SaveAsync();

                // Update Investigation Details in the repository
               _repository.InvestigationDetailsRepository.UpdatePatientInvestigationDetails(existingDetails);
                await _repository.SaveAsync();

                // Commit transaction
                await _repository.CommitTransaction(cancellationToken);

                // Map to DTO and return success response
                var patientInvestigationDtoResult = _mapper.Map<PatientInvestigationDto>(patientInvestigation);
                return new ApiOkResponse<PatientInvestigationDto>(patientInvestigationDtoResult, "Patient Investigation Updated Successfully");
            }
            catch (Exception ex)
            {
                await _repository.Rollback(cancellationToken);
                return new ApiErrorResponse("Something Went Wrong", ex.Message);
            }
        }

        private async Task<bool> DeletePatientInvestigationDetail(Guid detialId)
        {
            var result = await _repository.InvestigationDetailsRepository.GetPatientInvestigationDetailById(detialId, false);
            if (result == null) 
            { 
                return false;
            }
            result.IsActive = false;
            result.UpdatedAt = DateTime.Now;
            _repository.InvestigationDetailsRepository.UpdateSinglePatientInvestigationDetails(result);
            await _repository.SaveAsync();
           
            return true;
        }
    }
}

