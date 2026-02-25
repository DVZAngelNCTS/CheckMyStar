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
        /// Retrieves a specific assessment by its identifier.
        /// </summary>
        /// <param name="request">The request containing the assessment identifier.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the assessment details.</returns>
        [HttpGet("getassessment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetAssessment([FromQuery] AssessmentGetRequest request, CancellationToken ct)
        {
            var assessment = await assessmentService.GetAssessment(request, ct);

            return Ok(assessment);
        }

        /// <summary>
        /// Retrieves an assessment by folder identifier.
        /// </summary>
        /// <param name="request">The request containing the folder identifier.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the assessment details for the specified folder.</returns>
        [HttpGet("getassessmentbyfolder")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetAssessmentByFolder([FromQuery] AssessmentGetByFolderRequest request, CancellationToken ct)
        {
            var assessment = await assessmentService.GetAssessmentByFolder(request, ct);

            return Ok(assessment);
        }

        /// <summary>
        /// Retrieves all criteria for a specific assessment with their details (read-only).
        /// </summary>
        /// <remarks>
        /// Returns a complete list of all criteria associated with the specified assessment,
        /// including the criterion description, base points, actual points earned, status,
        /// validation state, and any comments. This endpoint is intended for read-only purposes
        /// to reconstruct the assessment evaluation table.
        /// </remarks>
        /// <param name="request">The request containing the assessment identifier.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing a list of assessment criteria with full details.</returns>
        [HttpGet("getassessmentcriteria")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> GetAssessmentCriteria([FromQuery] AssessmentCriteriaGetRequest request, CancellationToken ct)
        {
            var criteria = await assessmentService.GetAssessmentCriteria(request, ct);

            return Ok(criteria);
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
        [HttpPost("addassessment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> AddAssessment([FromBody] AssessmentSaveRequest request, CancellationToken ct)
        {
            var assessment = await assessmentService.AddAssessment(request, ct);

            return Ok(assessment);
        }

        /// <summary>
        /// Updates an existing assessment with its associated criteria.
        /// </summary>
        /// <remarks>
        /// Updates an assessment with all required fields. The identifier must correspond to an 
        /// existing assessment. The created date is preserved, and the updated date is set automatically. 
        /// The request must include the complete list of assessment criteria with their respective 
        /// criterion IDs, points, status, validation state, and optional comments.
        /// </remarks>
        /// <param name="request">The assessment details including criteria. Must not be null and must include a valid identifier.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>An <see cref="IActionResult"/> containing the updated assessment.</returns>
        [HttpPut("updateassessment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> UpdateAssessment([FromBody] AssessmentSaveRequest request, CancellationToken ct)
        {
            var assessment = await assessmentService.UpdateAssessment(request, ct);

            return Ok(assessment);
        }

        /// <summary>
        /// Deletes an assessment specified by the provided request.
        /// </summary>
        /// <remarks>This method requires the caller to have either the Administrator or Inspector role.
        /// The operation is performed asynchronously and returns an appropriate response based on the result.</remarks>
        /// <param name="request">The request containing the details and identifier of the assessment to be deleted.</param>
        /// <param name="ct">A cancellation token that can be used to cancel the delete operation.</param>
        /// <returns>An IActionResult that indicates the outcome of the delete operation.</returns>
        [HttpDelete("deleteassessment")]
        [Authorize(Roles = "Administrator, Inspector")]
        public async Task<IActionResult> DeleteAssessment(AssessmentDeleteRequest request, CancellationToken ct)
        {
            var result = await assessmentService.DeleteAssessment(request, ct);

            return Ok(result);
        }
    }
}
