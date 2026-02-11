using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

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
    [HttpPost("getstarcriteriastatus")]
    [Authorize(Roles = "Administrator")]
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
    [HttpPost("getstarcriteriadetails")]
    [Authorize(Roles = "Administrator")]
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
}
