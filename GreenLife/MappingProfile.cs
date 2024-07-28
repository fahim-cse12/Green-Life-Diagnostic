﻿using AutoMapper;
using Entities.Models;
using Shared.DataTransferObject;

namespace GreenLife
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserForRegistrationDto, User>().ReverseMap();
            CreateMap<DoctorDto, Doctor>().ReverseMap();
            CreateMap<DoctorCreateDto, Doctor>().ReverseMap();
            CreateMap<PatientDto, Patient>().ReverseMap();
            CreateMap<PatientCreateDto, Patient>().ReverseMap();
            CreateMap<PatientInvestigationDto, PatientInvestigation>().ReverseMap();
            CreateMap<PatientInvestigationCreateDto, PatientInvestigation>().ReverseMap();
            CreateMap<InvestigationDto, Investigation>().ReverseMap();
            CreateMap<InvestigationCreateDto, Investigation>().ReverseMap();
            CreateMap<TicketDto, Ticket>().ReverseMap();
            CreateMap<TicketCreateDto, Ticket>().ReverseMap();
            CreateMap<FinancialRecordDto, FinancialRecord>().ReverseMap();
            CreateMap<FinanceRecordCreateDto, FinancialRecord>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            
        } 
       
    }
}
