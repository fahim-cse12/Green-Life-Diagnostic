using Entities.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<ApiBaseResponse> GetAllRolesAsync()
        {
            var roles = await _roleManager.Roles
                     .Select(r => new RoleDto(
                         r.Id ?? string.Empty,
                         r.Name ?? string.Empty
                     ))
                     .ToListAsync();

            return new ApiOkResponse<List<RoleDto>>(roles, "Get All Roles successfully");

        }

        //public async Task<IEnumerable<IdentityRole>> GetAllRolesAsync()
        //{
        //    return await _roleManager.Roles.ToListAsync();
        //}

        public async Task<IdentityRole?> GetRoleByAsync(string roleId)
        {
            return await _roleManager.FindByIdAsync(roleId);
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Role '{roleName}' already exists."
                });
            }

            var role = new IdentityRole(roleName);
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> UpdateRoleAsync(string roleId, string roleName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Role '{roleName}' not found."
                });
            }

            role.Name = roleName;
            role.NormalizedName = roleName.ToUpper();
            return await _roleManager.UpdateAsync(role);
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = $"Role not found."
                });
            }
            return await _roleManager.DeleteAsync(role);

        }


    }
}
