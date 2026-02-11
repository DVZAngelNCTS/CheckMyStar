using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Apis.Services.Models;
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
    /// Create a new criterion
    /// </summary>
    /// <param name="model">Criterion data</param>
    /// <param name="ct">Cancellation token</param>
    [HttpPost("createcriterion")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateCriterion([FromBody] CreateCriterionModel model, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await criteriaService.CreateCriterionAsync(model, ct);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(result);
    }
}
