﻿using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Entities.Responses;
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
        public DoctorService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<DoctorDto> CreateDoctorAsync(DoctorDto doctorDto)
        {
            var doctorEntity = _mapper.Map<Doctor>(doctorDto);
            _repository.Doctor.CreateDoctor(doctorEntity);
            await _repository.SaveAsync();
            var doctorToReturn = _mapper.Map<DoctorDto>(doctorEntity);
            return doctorToReturn;
        }

        public async Task DeleteDoctorAsync(Guid doctorId, bool trackChanges)
        {
            var doctor = await _repository.Doctor.GetDoctorAsync(doctorId, trackChanges);
            doctor.Status = false;
            await _repository.SaveAsync();
        }

        public async Task<ApiBaseResponse> GetAllDoctorAsync(bool trackChanges)
        {
            var doctors = await _repository.Doctor.GetAllDoctorAsync(trackChanges);
            var doctorsDto = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            return new ApiOkResponse<IEnumerable<DoctorDto>>(doctorsDto);
        }      
        public async Task<ApiBaseResponse> GetDoctorAsync(Guid doctorId, bool trackChanges)
        {
            var doctor = await _repository.Doctor.GetDoctorAsync(doctorId, trackChanges);
            if (doctor is null)
                return new IdNotFoundResponse<Doctor>(doctorId);
            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return new ApiOkResponse<DoctorDto>(doctorDto);
        }

        public async Task UpdateDoctorAsync(Guid doctorId, DoctorDto doctorDto, bool trackChanges)
        {
            var doctor = await _repository.Doctor.GetDoctorAsync(doctorId, trackChanges);
            if (doctor is null)
                throw new IdNotFoundException<Doctor>(doctorId);
            _mapper.Map(doctorDto, doctor);
            await _repository.SaveAsync();
        }

       
    }
}
