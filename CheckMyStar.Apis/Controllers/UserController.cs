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
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("getusers")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userService.GetUsers();
            
            return Ok(users);
        }
    }
}
