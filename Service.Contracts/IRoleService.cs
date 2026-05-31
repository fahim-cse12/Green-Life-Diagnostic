using Entities.Responses;
using Microsoft.AspNetCore.Identity;

namespace Service.Contracts
{
    public interface IRoleService
    {
        Task<ApiBaseResponse> GetAllRolesAsync();
        Task<IdentityRole?> GetRoleByAsync(string roleId);
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<IdentityResult> UpdateRoleAsync(string roleId, string roleName);
        Task<IdentityResult> DeleteRoleAsync(string roleId);
    }
}


