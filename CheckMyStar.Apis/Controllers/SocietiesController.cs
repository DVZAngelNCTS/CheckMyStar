using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;

/// <summary>
/// 
/// </summary>
/// <param name="societyService"></param>
[ApiController]
[Route("api/[controller]")]
public class SocietiesController(ISocietyService societyService) : ControllerBase
{
    /// <summary>
    /// Crée une nouvelle société.
    /// </summary>
    /// <param name="request">Données de la société</param>
    /// <param name="ct">Jeton d'annulation</param>
    /// <returns>Résultat de la création</returns>
    [HttpPost("addSociety")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateSociety([FromBody] SocietyCreateRequest request, CancellationToken ct)
    {
        var result = await societyService.CreateSociety(request, ct);

        return Ok(result);
    }

    /// <summary>
    /// Récupère toutes les sociétés.
    /// </summary>
    /// <param name="ct">Jeton d'annulation</param>
    [HttpGet("getSocieties")]
    [Authorize(Roles = "Administrator, Inspector")]
    public async Task<IActionResult> GetSocieties(CancellationToken ct)
    {
        var result = await societyService.GetSocieties(ct);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}