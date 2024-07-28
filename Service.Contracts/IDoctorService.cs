using Entities.Responses;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IDoctorService
    {
        Task<ApiBaseResponse> GetAllDoctorAsync(bool trackChanges);
        Task<ApiBaseResponse> GetDoctorAsync(Guid doctorId, bool trackChanges);
        Task<ApiBaseResponse> CreateDoctorAsync(DoctorCreateDto doctorDto);
        Task<ApiBaseResponse> DeleteDoctorAsync(Guid doctorId, bool trackChanges);
        Task<ApiBaseResponse> UpdateDoctorAsync(Guid doctorId, DoctorCreateDto doctorDto, bool trackChanges);
    }
}
