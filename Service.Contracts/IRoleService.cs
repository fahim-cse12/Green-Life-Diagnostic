using Entities.Models;
using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


