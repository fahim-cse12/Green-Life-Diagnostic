using AutoMapper;
using Entities.Models;
using Shared.DataTransferObject;

namespace GreenLife
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mappings
            CreateMap<UserForRegistrationDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();

            // Doctor Mappings
            CreateMap<DoctorDto, Doctor>().ReverseMap();
            CreateMap<DoctorCreateDto, Doctor>().ReverseMap();

            // Patient Mappings
            CreateMap<PatientDto, Patient>().ReverseMap();
            CreateMap<PatientCreateDto, Patient>().ReverseMap();

            // Patient Investigation Mappings
            CreateMap<PatientInvestigation, PatientInvestigationDto>()
                .ForMember(dest => dest.InvestigationDetails, opt => opt.MapFrom(src => src.InvestigationDetails)); // Map InvestigationDetails collection

            CreateMap<PatientInvestigationDetail, PatientInvestigationDetailDto>()
                .ForMember(dest => dest.InvestigationName, opt => opt.MapFrom(src => src.Investigation.InvestigationName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Investigation.Description))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd-MMM-yyyy"))) // Format dates
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("dd-MMM-yyyy")));

            CreateMap<PatientInvestigationDetailUpdateDto, PatientInvestigationDetail>().ReverseMap();                    // Ignoring if this is set elsewhere

            CreateMap<PatientInvestigationCreateDto, PatientInvestigation>().ReverseMap();

            CreateMap<PatientInvestigationDetailCreateDto, PatientInvestigationDetail>()
                .ForMember(dest => dest.PatientInvestigationDetailId, opt => opt.Ignore()) // Ignoring auto-generated fields
                .ForMember(dest => dest.PatientInvestigationId, opt => opt.Ignore());      // Ignoring if this is set elsewhere

            // Investigation Mappings
            CreateMap<InvestigationDto, Investigation>().ReverseMap();
            CreateMap<InvestigationCreateDto, Investigation>().ReverseMap();

            // Ticket Mappings
            CreateMap<TicketDto, Ticket>().ReverseMap();
            CreateMap<TicketCreateDto, Ticket>().ReverseMap();

            // Financial Record Mappings
            CreateMap<FinancialRecordDto, FinancialRecord>().ReverseMap();
            CreateMap<FinanceRecordCreateDto, FinancialRecord>().ReverseMap();
        }
    }

}
