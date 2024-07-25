using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.Responses;
using FluentValidation;
using FluentValidation.Results;
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
    internal sealed class PatientService : IPatientService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<Patient> _patientValidator;
        private IRepositoryManager _repositoryManager;

        public PatientService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
            IValidator<Patient> patientValidator)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _patientValidator = patientValidator;
        }

        public async Task<ApiBaseResponse> PurchageTicketAsync(PurchageTicketDto purchageTicket)
        {
            dynamic errorMessages = null;
            var patientEntity = _mapper.Map<Patient>(purchageTicket.patientDto);
            var validationResultForPatient = await _patientValidator.ValidateAsync(patientEntity);
            if (!validationResultForPatient.IsValid)
            {
                errorMessages.add(validationResultForPatient.Errors.Select(e => e.ErrorMessage).ToList());
            }
            var ticketEntity = _mapper.Map<Ticket>(purchageTicket.ticketDto);
           
            if (ticketEntity.Amount == 0)
            {
                errorMessages.Add("doctor fee is required") ;
            }

            if (errorMessages != null)
            {
                return new ApiErrorResponse("Validation failed", errorMessages);
            }
            CancellationToken cancellationToken = default;
            await _repository.BeginTransaction(cancellationToken);
            try
            {
                _repository.Patient.CreatePatient(patientEntity);
                await _repository.SaveAsync();

                ticketEntity.PatientId = patientEntity.Id;

                _repository.Ticket.CreateTicket(ticketEntity);
                await _repository.SaveAsync();

                await _repository.CommitTransaction(cancellationToken);

                return new ApiOkResponse<string>(patientEntity.Name, $"Purchage Ticket confirmed for Patient: {patientEntity.Name}");
            }
            catch (Exception ex)
            {
                await _repository.Rollback(cancellationToken);
                return new ApiOkResponse<string>("Something Went Wrong", ex.Message);
            }
           

        }
        
    }
}
