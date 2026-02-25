using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Defines API endpoints for assessment result operations.
    /// </summary>
    /// <param name="assessmentResultService">Assessment result service</param>
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentResultController(IAssessmentResultService assessmentResultService) : ControllerBase
    {
        /// <summary>
        /// Retrieves the next available identifier for a new assessment result.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the next identifier.</returns>
        [HttpGet("getnextidentifier")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetNextIdentifier(CancellationToken ct)
        {
            var response = await assessmentResultService.GetNextIdentifier(ct);

            return Ok(response);
        }

        /// <summary>
        /// Creates a new assessment result.
        /// </summary>
        /// <remarks>
        /// Creates an assessment result with all required fields including the assessment identifier,
        /// acceptance status, mandatory and optional points earned, thresholds, and ONC failure count.
        /// </remarks>
        /// <param name="request">The assessment result details. Must not be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created assessment result.</returns>
        [HttpPost("addassessmentresult")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> AddAssessmentResult([FromBody] AssessmentResultSaveRequest request, CancellationToken ct)
        {
            var response = await assessmentResultService.AddAssessmentResult(request, ct);

            return Ok(response);
        }

        /// <summary>
        /// Updates an existing assessment result.
        /// </summary>
        /// <remarks>
        /// Updates an assessment result with all fields including the assessment identifier,
        /// acceptance status, mandatory and optional points earned, thresholds, and ONC failure count.
        /// The UpdatedDate field will be automatically set.
        /// </remarks>
        /// <param name="request">The assessment result details. Must not be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated assessment result.</returns>
        [HttpPut("updateassessmentresult")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> UpdateAssessmentResult([FromBody] AssessmentResultSaveRequest request, CancellationToken ct)
        {
            var response = await assessmentResultService.UpdateAssessmentResult(request, ct);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all assessment results for a specific folder.
        /// </summary>
        /// <param name="request">The request containing the folder identifier.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing a list of assessment results for the folder.</returns>
        [HttpGet("getassessmentresultsbyfolder")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetAssessmentResultsByFolder([FromQuery] AssessmentResultGetByFolderRequest request, CancellationToken ct)
        {
            var response = await assessmentResultService.GetAssessmentResultsByFolder(request, ct);

            return Ok(response);
        }
    }
}
