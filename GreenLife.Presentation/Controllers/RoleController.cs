using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenLife.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public RoleController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _serviceManager.roleService.GetAllRolesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _serviceManager.roleService.GetRoleByAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string roleName)
        {
            var result = await _serviceManager.roleService.CreateRoleAsync(roleName);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Role created successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] string roleName)
        {
            var result = await _serviceManager.roleService.UpdateRoleAsync(id, roleName);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Role updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _serviceManager.roleService.DeleteRoleAsync(id);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return Ok("Role deleted successfully");
        }
    }
}
