using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Defines API endpoints for role operations.
    /// </summary>
    /// <param name="roleService"></param>
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        /// <summary>
        /// Get roles
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="ct">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("getroles")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetRoles(RoleGetRequest request, CancellationToken ct)
        {
            var roles = await roleService.GetRoles(request, ct);

            return Ok(roles);
        }
    }
}
