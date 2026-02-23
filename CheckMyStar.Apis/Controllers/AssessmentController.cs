using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Defines API endpoints for assessment operations.
    /// </summary>
    /// <param name="assessmentService">Assessment service</param>
    [ApiController]
    [Route("api/[controller]")]
    public class AssessmentController(IAssessmentService assessmentService) : ControllerBase
    {
        /// <summary>
        /// Retrieves all assessments.
        /// </summary>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing a list of all assessments.</returns>
        [HttpGet("getassessments")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetAssessments(CancellationToken ct)
        {
            var assessments = await assessmentService.GetAssessments(ct);

            return Ok(assessments);
        }

        /// <summary>
        /// Creates a new assessment with its associated criteria.
        /// </summary>
        /// <remarks>
        /// Creates an assessment with all required fields. The identifier, created date, 
        /// and updated date are generated automatically. The request must include a list 
        /// of assessment criteria with their respective criterion IDs, points, status, 
        /// validation state, and optional comments.
        /// </remarks>
        /// <param name="request">The assessment details including criteria. Must not be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created assessment with its generated identifier.</returns>
        [HttpPost("createassessment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> CreateAssessment([FromBody] AssessmentCreateRequest request, CancellationToken ct)
        {
            var assessment = await assessmentService.CreateAssessment(request, ct);

            return Ok(assessment);
        }

        /// <summary>
        /// Updates an existing assessment with its associated criteria.
        /// </summary>
        /// <remarks>
        /// Updates an assessment with all required fields. The updated date is automatically set to the current date and time.
        /// All existing criteria are replaced with the new ones provided in the request.
        /// </remarks>
        /// <param name="request">The assessment details including the identifier and updated criteria. Must not be null.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated assessment.</returns>
        [HttpPut("updateassessment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> UpdateAssessment([FromBody] AssessmentUpdateRequest request, CancellationToken ct)
        {
            var assessment = await assessmentService.UpdateAssessment(request, ct);

            if (!assessment.IsSuccess)
            {
                return NotFound(assessment);
            }

            return Ok(assessment);
        }

        /// <summary>
        /// Deletes an assessment by its identifier.
        /// </summary>
        /// <remarks>
        /// Deletes the assessment with the specified ID along with all its associated criteria.
        /// </remarks>
        /// <param name="id">The identifier of the assessment to delete.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the deletion operation.</returns>
        [HttpDelete("deleteassessment/{id}")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> DeleteAssessment(int id, CancellationToken ct)
        {
            var result = await assessmentService.DeleteAssessment(id, ct);

            if (!result.IsSuccess)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
