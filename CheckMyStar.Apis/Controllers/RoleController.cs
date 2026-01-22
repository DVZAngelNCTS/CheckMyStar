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
        [HttpPost("getroles")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetRoles([FromBody] RoleGetRequest request, CancellationToken ct)
        {
            var roles = await roleService.GetRoles(request, ct);

            return Ok(roles);
        }

        /// <summary>
        /// Creates a new role using the specified request data.
        /// </summary>
        /// <param name="request">The details of the role to create. Must not be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created role.</returns>
        [HttpPost("addrole")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> AddRole([FromBody] RoleSaveRequest request, CancellationToken ct)
        {
            var role = await roleService.AddRole(request, ct);

            return Ok(role);
        }

        /// <summary>
        /// Updates an existing role with the specified details.
        /// </summary>
        /// <param name="request">The role information to update. Must not be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated role information if the update is successful.</returns>
        [HttpPost("updaterole")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateRole([FromBody] RoleSaveRequest request, CancellationToken ct)
        {
            var role = await roleService.UpdateRole(request, ct);

            return Ok(role);
        }

        /// <summary>
        /// Deletes an existing role based on the specified request.
        /// </summary>
        /// <remarks>This action is restricted to users with the Administrator role. The request must
        /// include valid role information in the request body.</remarks>
        /// <param name="request">The details of the role to delete. Must not be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An IActionResult indicating the result of the delete operation.</returns>
        [HttpPost("deleterole")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteRole([FromBody] RoleDeleteRequest request, CancellationToken ct)
        {
            await roleService.DeleteRole(request, ct);

            return Ok();
        }
    }
}
