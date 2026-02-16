using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [HttpPost("getSocieties")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetSocieties(CancellationToken ct)
    {
        var result = await societyService.GetSocieties(ct);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}