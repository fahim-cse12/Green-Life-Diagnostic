using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using FluentValidation;
using LoggerService;
using Service.Contracts;
using Shared.DataTransferObject;

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
                ticketEntity.Status = true;
                ticketEntity.CreatedAt = DateTime.Now;

                _repository.Ticket.CreateTicket(ticketEntity);
                await _repository.SaveAsync();
                var doctor = await _repository.Doctor.GetDoctorAsync(ticketEntity.DoctorId, false);
                var financialRecord = new FinancialRecord
                {
                    Income = ticketEntity.Amount,
                    Purpose = $"Ticket purchage for doctor {doctor.Name}",
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
    }
}
