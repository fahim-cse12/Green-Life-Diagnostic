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
    internal sealed class DoctorService : IDoctorService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<Doctor> _validator;

        public DoctorService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
            IValidator<Doctor> validator)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _validator = validator; 
        }

        public async Task<ApiBaseResponse> CreateDoctorAsync(DoctorCreateDto doctorDto)
        {
            var doctorEntity = _mapper.Map<Doctor>(doctorDto);
            var validationResult = await _validator.ValidateAsync(doctorEntity);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiErrorResponse("Validation failed", errorMessages);
            }
            doctorEntity.Status = true;
            doctorEntity.CreatedAt = DateTime.Now;
            _repository.Doctor.CreateDoctor(doctorEntity);
            await _repository.SaveAsync();

            var doctorToReturn = _mapper.Map<DoctorDto>(doctorEntity);
            return new ApiOkResponse<DoctorDto>(doctorToReturn, "Doctor created successfully");

        }

        public async Task<ApiBaseResponse> GetAllDoctorAsync(bool trackChanges)
        {
            var doctorList = await _repository.Doctor.GetAllDoctorAsync(trackChanges);

            var doctorsDto = _mapper.Map<IEnumerable<DoctorDto>>(doctorList);
            
            return new ApiOkResponse<IEnumerable<DoctorDto>>(doctorsDto, "");
        }
        public async Task<ApiBaseResponse> GetDoctorAsync(Guid doctorId, bool trackChanges)
        {
            var doctor = await _repository.Doctor.GetDoctorAsync(doctorId, trackChanges);
            if (doctor is null)
                return new IdNotFoundResponse<Doctor>(doctorId);
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return new ApiOkResponse<DoctorDto>(doctorDto, "");
        }

        public async Task<ApiBaseResponse> DeleteDoctorAsync(Guid doctorId, bool trackChanges)
        {
            var doctor = await _repository.Doctor.GetDoctorAsync(doctorId, trackChanges);
            if (doctor is null)
                return new IdNotFoundResponse<Doctor>(doctorId);
            doctor.Status = false;
            doctor.UpdatedAt = DateTime.Now;
            _repository.Doctor.UpdateDoctor(doctor);
            await _repository.SaveAsync();
            return new ApiOkResponse<Guid>(doctorId, "Doctor Deleted Successfully");
        }

        public async Task<ApiBaseResponse> UpdateDoctorAsync(Guid doctorId, DoctorCreateDto doctorDto, bool trackChanges)
        {
            var doctorEntity = _mapper.Map<Doctor>(doctorDto);
            var validationResult = await _validator.ValidateAsync(doctorEntity);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new ApiErrorResponse("Validation failed", errorMessages);
            }

            var doctor = await _repository.Doctor.GetDoctorAsync(doctorId, trackChanges);
            if (doctor is null)
                throw new IdNotFoundException<Doctor>(doctorId);
            doctor.UpdatedAt = DateTime.Now;
            _mapper.Map(doctorDto, doctor);
            await _repository.SaveAsync();
            return new ApiOkResponse<Guid>(doctorId, "Doctor Updated Successfully");
        }


    }
}
