using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using CheckMyStar.Apis.Services.Abstractions;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Defines API endpoints for dashboard operations.
    /// </summary>
    /// <param name="dashboardService">Dashboard service</param>
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController(IDashboardService dashboardService) : ControllerBase
    {
        /// <summary>
        /// Get dashboard
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>Dashboard</returns>
        [HttpGet("getdashboard")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetDashboard(CancellationToken ct)
        {
            var dashboard = await dashboardService.GetDashboard(ct);

            return Ok(dashboard);
        }
    }
}
