﻿using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using FluentValidation;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObject;
using System.Diagnostics;
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
                //Save financial record
                SaveFinancialRecord(patientInvestigationDto.TotalAmount, patientInvestigationDto.PatientInvestigationUniqueId);

                return new ApiOkResponse<PatientInvestigationDto>(patientInvestigationDto, "Patient Investigation Created Successfully");
            }
            catch (Exception ex)
            {
                await _repository.Rollback(cancellationToken);
                return new ApiErrorResponse("Something Went Wrong", ex.Message);
            }
        }

        private async Task<bool> SaveFinancialRecord(decimal payamount, string uniqueId)
        {
            var financialRecord = new FinancialRecord
            {
                Income = payamount,
                UniqueId = uniqueId,
                Purpose = $"From Ticket or Investigation UniqueId: {uniqueId}",
                Status = true,
                RecordDate = DateTime.Now,
                CreatedAt = DateTime.Now
            };
            _repository.FinancialRecord.CreateFinancialRecord(financialRecord);
            await _repository.SaveAsync();

            return true;
        }

        public async Task<ApiBaseResponse> DeletePatientInvestigationDetailAsync(Guid patientInvestigaionId ,Guid detailId)
        {
            try
            {
                var patientInvestigaionResult = await _repository.PatientInvestigation.GetPatientInvestigationById(patientInvestigaionId, false);
                var isExist = patientInvestigaionResult.InvestigationDetails.FirstOrDefault(i => i.PatientInvestigationDetailId.Equals(detailId));
                if(isExist == null)
                {
                    return new ApiErrorResponse("Not Found", new List<string> { "This Investigation detail not belong to this Patient Investigaion" });
                }
                //var result = await _repository.InvestigationDetailsRepository.GetPatientInvestigationDetailById(detailId, false);
                //if (result == null)
                //{
                //    return new ApiErrorResponse("Not Found", new List<string> { "Patient Investigation not found" });
                //}
                if (!string.IsNullOrEmpty(isExist.ResultText))
                {
                    return new ApiErrorResponse("Update failed", $"{isExist.PatientInvestigationDetailId} This investigation's result is created it cannot be deleted");
                }
                _repository.InvestigationDetailsRepository.DeleteSinglePatientInvestigationDetails(isExist);
                await _repository.SaveAsync();

                UpdateFinancialRecord(patientInvestigaionResult, isExist.PaymentAmount);

                return new ApiOkResponse<Guid>(detailId, "Patient Investigation Detail Deleted Successfully");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
            
        }
        private async Task<bool> UpdateFinancialRecord(PatientInvestigation patientInvestigation, decimal deductAmount)
        {
            patientInvestigation.PaidAmount = patientInvestigation.PaidAmount - deductAmount;
         
            var existingFinancialRecord = await _repository.FinancialRecord.GetFinancialRecordsByConditionAsync(i=> i.UniqueId.Equals(patientInvestigation.PatientInvestigationUniqueId), false);

            existingFinancialRecord.Income = patientInvestigation.PaidAmount;
            existingFinancialRecord.UpdatedAt = DateTime.Now;
            existingFinancialRecord.UpdatedBy = null;

            _repository.FinancialRecord.UpdateFinancialRecord(existingFinancialRecord);
            await _repository.SaveAsync();
                      
            return true;
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

        public async Task<ApiBaseResponse> UpdatePatientInvestigationAsync(PatientInvestigationUpdateDto patientInvestigationUpdateDto)
        {
            CancellationToken cancellationToken = default;
            var currentDate = DateTime.Now;

            if (!patientInvestigationUpdateDto.PatientInvestigationDetailUpdateDtos.Any())
            {
                return new ApiErrorResponse("Validation failed", new List<string> { "At least one investigation should be added" });
            }

            // Fetch existing patient investigation
            var patientInvestigation = await _repository.PatientInvestigation.GetPatientInvestigationById(patientInvestigationUpdateDto.PatientInvestigationId, false);
            if (patientInvestigation == null)
            {
                return new ApiErrorResponse("Not Found", new List<string> { "Patient Investigation not found" });
            }

            // Map the updated PatientInvestigation from DTO
            _mapper.Map(patientInvestigationUpdateDto, patientInvestigation);
            patientInvestigation.UpdatedAt = currentDate;

            await _repository.BeginTransaction(cancellationToken);
            try
            {
                _repository.PatientInvestigation.UpdatePatientInvestigation(patientInvestigation);
               

                foreach (var detail in patientInvestigationUpdateDto.PatientInvestigationDetailUpdateDtos)
                {
                    // For update on existing
                    if (detail.PatientInvestigationDetailId != null)
                    {
                        var existingDetail = await _repository.InvestigationDetailsRepository.GetPatientInvestigationDetailById(detail.PatientInvestigationDetailId, false);
                        if (existingDetail != null)
                        {
                            var updateDetail = _mapper.Map(detail, existingDetail);
                            updateDetail.UpdatedAt = currentDate;
                            var detailValidationResult = await _patientInvestigationDetailValidator.ValidateAsync(updateDetail);
                            if (!detailValidationResult.IsValid)
                            {
                                return new ApiErrorResponse("Validation failed", detailValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
                            }
                            _repository.InvestigationDetailsRepository.UpdateSinglePatientInvestigationDetails(updateDetail);

                        }
                        else
                        {
                            return new ApiErrorResponse("Not Found", new List<string> { "Patient Investigation Details not found" });
                        }

                    }
                    else
                    {
                        var newDetail = _mapper.Map<PatientInvestigationDetail>(detail);
                        newDetail.CreatedAt = currentDate;
                        newDetail.CreatedAt = currentDate;
                        newDetail.UpdatedAt = currentDate;
                        newDetail.IsDelivered = false;
                        newDetail.IsActive = true;

                        _repository.InvestigationDetailsRepository.CreateSinglePatientInvestigationDetails(newDetail);
                    }

                    //await _repository.SaveAsync();
                }
                await _repository.SaveAsync();
                await _repository.CommitTransaction(cancellationToken);

                // Map to DTO and return success response
                var patientInvestigationDtoResult = _mapper.Map<PatientInvestigationDto>(patientInvestigation);

                SaveFinancialRecord(patientInvestigationDtoResult.TotalAmount, patientInvestigationDtoResult.PatientInvestigationUniqueId);

                return new ApiOkResponse<PatientInvestigationDto>(patientInvestigationDtoResult, "Patient Investigation Updated Successfully");
            }
            catch (Exception ex)
            {
                await _repository.Rollback(cancellationToken);
                return new ApiErrorResponse("Something Went Wrong", ex.Message);
            }                      
            
        }

        //private async Task<bool> DeletePatientInvestigationDetail(Guid detialId)
        //{
        //    var result = await _repository.InvestigationDetailsRepository.GetPatientInvestigationDetailById(detialId, false);
        //    if (result == null) 
        //    { 
        //        return false;
        //    }

        //    _repository.InvestigationDetailsRepository.DeleteSinglePatientInvestigationDetails(result);
        //    await _repository.SaveAsync();
           
        //    return true;
        //}
    }
}

