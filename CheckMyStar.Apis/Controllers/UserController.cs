using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Defines API endpoints for user operations.
    /// </summary>
    /// <param name="userService"></param>    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        /// <summary>
        /// Get users
        /// </summary>
        /// <param name="ct">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet("getusers")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers(CancellationToken ct)
        {
            var users = await userService.GetUsers(ct);
            
            return Ok(users);
        }
    }
}
