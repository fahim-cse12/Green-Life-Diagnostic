using AutoMapper;
using Entities.Models;
using Shared.DataTransferObject;

namespace GreenLife
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserForRegistrationDto, User>();
        } 
       
    }
}
