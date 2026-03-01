using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Defines API endpoints for criteria operations.
/// </summary>
/// <param name="criteriaService">Criteria service</param>
[ApiController]
[Route("api/[controller]")]
public class CriteriaController(ICriteriaService criteriaService) : ControllerBase
{
    /// <summary>
    /// Get star criteria status
    /// </summary>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>Criteria status</returns>
    [HttpGet("getstarcriteriastatus")]
    [Authorize(Roles = "Administrator, Inspector")]
    public async Task<IActionResult> GetStarCriteriaStatus(CancellationToken ct)
    {
        var criterias = await criteriaService.GetStarCriteriaStatus(ct);

        return Ok(criterias);
    }

    /// <summary>
    /// Get star criteria details
    /// </summary>
    /// <param name="ct">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>Criteria details</returns>
    [HttpGet("getstarcriteriadetails")]
    [Authorize(Roles = "Administrator, Inspector")]
    public async Task<IActionResult> GetStarCriteriaDetails(CancellationToken ct)
    {
        var result = await criteriaService.GetStarCriteriaDetails(ct);

        return Ok(result);
    }

    /// <summary>
    /// Add a new criterion
    /// </summary>
    /// <param name="request">Criterion request</param>
    /// <param name="ct">Cancellation token</param>
    [HttpPost("addcriterion")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> AddCriterion([FromBody] CriterionSaveRequest request, CancellationToken ct)
    {
        var result = await criteriaService.AddCriterion(request, ct);

        return Ok(result);
    }

    /// <summary>
    /// Deletes a criterion identified by the specified request parameters.
    /// </summary>
    /// <remarks>This method requires the caller to have Administrator role permissions. The deletion
    /// operation is asynchronous and may be canceled using the provided cancellation token.</remarks>
    /// <param name="request">The request containing the details of the criterion to be deleted, including the identifier of the criterion.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the operation if needed.</param>
    /// <returns>An IActionResult indicating the result of the deletion operation. Returns Ok if the deletion is successful;
    /// otherwise, returns BadRequest.</returns>
    [HttpDelete("deletecriterion")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteCriterion([FromQuery] CriterionDeleteRequest request, CancellationToken ct)
    {
        var result = await criteriaService.DeleteCriterion(request, ct);

        return Ok(result);
    }

    /// <summary>
    /// Updates an existing criterion.
    /// </summary>
    /// <param name="request">Updated data</param>
    /// <param name="ct">Cancellation token</param>
    [HttpPut("updatecriterion")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateCriterion([FromBody] CriterionUpdateRequest request, CancellationToken ct)
    {
        var result = await criteriaService.UpdateCriterion(request, ct);

        return Ok(result);
    }
}
