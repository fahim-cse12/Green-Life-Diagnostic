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
        // Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);
        Task<ApiBaseResponse> GetDoctorAsync(Guid doctorId, bool trackChanges);
        Task<DoctorDto> CreateDoctorAsync(DoctorDto doctorDto);
        Task DeleteDoctorAsync(Guid doctorId, bool trackChanges);
        Task UpdateDoctorAsync(Guid doctorId, DoctorDto doctorDto, bool trackChanges);
    }
}
