using AutoMapper;
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
            CreateMap<PatientDto, Patient>().ReverseMap();
            CreateMap<PatientInvestigationDto, PatientInvestigation>().ReverseMap();
            CreateMap<InvestigationDto, Investigation>().ReverseMap();
            CreateMap<TicketDto, Ticket>().ReverseMap();
            CreateMap<FinancialRecordDto, FinancialRecord>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            
        } 
       
    }
}
