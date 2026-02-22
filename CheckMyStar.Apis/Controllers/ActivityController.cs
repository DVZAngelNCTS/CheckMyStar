using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Defines API endpoints for activity operations.
    /// </summary>
    /// <param name="activityService">Activity service</param>
    [ApiController]
    [Route("api/[controller]")]
    public class ActivityController(IActivityService activityService) : ControllerBase
    {
        /// <summary>
        /// Get activities
        /// </summary>
        /// <param name="request">The criteria for retrieving activities. Must not be null</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>Activity</returns>
        [HttpGet("getactivities")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetActivities([FromQuery] ActivityGetRequest request, CancellationToken ct)
        {
            var activities = await activityService.GetActivities(request, ct);

            return Ok(activities);
        }

        /// <summary>
        /// Retrieves a list of activities that match the specified filter criteria.
        /// </summary>
        /// <remarks>This method is asynchronous and may take additional time to complete depending on the
        /// underlying data source. Handle cancellation appropriately to avoid unnecessary processing.</remarks>
        /// <param name="request">The request object containing parameters for filtering activities, such as date range or activity type.
        /// Cannot be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An IActionResult containing the list of activities. Returns a 200 OK response with the activities if
        /// successful.</returns>
        [HttpGet("getdetailactivities")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetActivities([FromQuery] ActivitiesGetRequest request, CancellationToken ct)
        {
            var activities = await activityService.GetActivities(request, ct);

            return Ok(activities);
        }
    }
}
