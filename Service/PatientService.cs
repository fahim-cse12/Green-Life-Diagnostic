using AutoMapper;
using Contracts;
using Dapper;
using Entities.Models;
using Entities.NonDbEntities;
using Entities.Responses;
using FluentValidation;
using LoggerService;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObject;
using Shared.Utility;
using System.Data;

namespace Service
{
    internal sealed class PatientService : IPatientService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<Patient> _patientValidator;

        public PatientService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
            IValidator<Patient> patientValidator)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _patientValidator = patientValidator;
        }

        public async Task<ApiBaseResponse> PurchageTicketAsync(PurchageTicketDto purchaseTicket)
        {
            var errorMessages = new List<string>();
            var patientEntity = _mapper.Map<Patient>(purchaseTicket.patientDto);
            var validationResultForPatient = await _patientValidator.ValidateAsync(patientEntity);

            if (!validationResultForPatient.IsValid)
            {
                errorMessages.AddRange(validationResultForPatient.Errors.Select(e => e.ErrorMessage));
            }

            var ticketEntity = _mapper.Map<Ticket>(purchaseTicket.ticketDto);

            if (ticketEntity.Amount == 0)
            {
                errorMessages.Add("Doctor fee is required");
            }

            if (errorMessages.Any())
            {
                return new ApiErrorResponse("Validation failed", errorMessages);
            }

            CancellationToken cancellationToken = default;
            await _repository.BeginTransaction(cancellationToken);

            try
            {
                patientEntity.Status = true;
                patientEntity.CreatedAt = DateTime.Now;
                _repository.Patient.CreatePatient(patientEntity);
                await _repository.SaveAsync();

                ticketEntity.PatientId = patientEntity.Id;
                ticketEntity.UniqueId = await GenerateUniqueIdAsync();
                ticketEntity.Status = true;
                ticketEntity.CreatedAt = DateTime.Now;

                _repository.Ticket.CreateTicket(ticketEntity);
                await _repository.SaveAsync();

                var financialRecord = new FinancialRecord
                {
                    Income = ticketEntity.Amount,
                    Purpose = $"Ticket purchage for Ticket or Investigation UniqueId: {ticketEntity.UniqueId}",
                    Status = true,
                    RecordDate = DateTime.Now,
                    CreatedAt = DateTime.Now
                };
                _repository.FinancialRecord.CreateFinancialRecord(financialRecord);
                await _repository.SaveAsync();
                await _repository.CommitTransaction(cancellationToken);

                return new ApiOkResponse<string>(patientEntity.Name, $"Purchase Ticket confirmed for Patient: {patientEntity.Name}");
            }
            catch (Exception ex)
            {
                await _repository.Rollback(cancellationToken);
                return new ApiErrorResponse("Something Went Wrong", ex.Message);
            }
        }

        private async Task<string> GenerateUniqueIdAsync()
        {
            var today = DateTime.Now;
            var datePart = today.ToString("ddMMyy");
            var lastTicket = await _repository.Ticket.FindTicketsByConditionAsync(t => t.UniqueId.StartsWith(datePart),false);

            int sequenceNumber = 1;

            if (lastTicket != null)
            {
                var lastSequenceString = lastTicket.UniqueId.Substring(7);
                if (int.TryParse(lastSequenceString, out int lastSequence))
                {
                    sequenceNumber = lastSequence + 1;
                }
            }

            return $"{datePart}T{sequenceNumber:D4}";
        }

        public async Task<ApiBaseResponse> PatientSearchByQuery(string patientName = null, string mobileNo = null, string doctorName = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            string sp = DatabaseProcedure.PatientSearchByQuery;
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@PatientName", string.IsNullOrEmpty(patientName) ? (object)DBNull.Value : patientName),
                new SqlParameter("@MobileNumber", string.IsNullOrEmpty(mobileNo) ? (object)DBNull.Value : mobileNo),
                new SqlParameter("@DoctorName", string.IsNullOrEmpty(doctorName) ? (object)DBNull.Value : doctorName),
                new SqlParameter("@StartDate", startDate.HasValue ? (object)startDate.Value : DBNull.Value),
                new SqlParameter("@EndDate", endDate.HasValue ? (object)endDate.Value : DBNull.Value)
            };

            var result = await _repository.ExecuteStoredProcedureToGetData<PatientHistoryDto>(sp, parameters).ToListAsync();

            if (!result.Any())
            {
                return new ApiOkResponse<IEnumerable<PatientHistoryDto>>(null, "Data not found");
            }

            return new ApiOkResponse<IEnumerable<PatientHistoryDto>>(result, "Patients retrieved successfully");
        }

        public async Task<ApiBaseResponse> DeletePurchasedTicketAsync(Guid ticketId, bool trackChanges)
        {
            string sp = DatabaseProcedure.TicketDeleteQuery;
            var inputParameters = new DynamicParameters();
            inputParameters.Add("@TicketId", ticketId);

            var result = await _repository.ExecuteStoreProcedure(sp, inputParameters);
            
            return new ApiOkResponse<string>(null, result);
        }
       
    }
}
