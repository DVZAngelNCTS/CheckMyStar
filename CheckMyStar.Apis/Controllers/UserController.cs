using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

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
        /// <param name="request"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpPost("getusers")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers([FromBody] UserGetRequest request, CancellationToken ct)
        {
            var users = await userService.GetUsers(request, ct);
            
            return Ok(users);
        }
    }
}
