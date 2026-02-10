using CheckMyStar.Bll.Criteres;
using CheckMyStar.Bll.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/criteres")]
[Authorize(Roles = "Administrator")]
public class CriteresController(ICriteresService criteresService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<IEnumerable<StarCriteriaDto>>> Get()
    {
        var result = await criteresService.GetStarCriteriaAsync().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpPost("details")]
    public async Task<ActionResult<IEnumerable<StarCriteriaDetailDto>>> GetDetails()
    {
        var result = await criteresService.GetStarCriteriaDetailsAsync().ConfigureAwait(false);
        return Ok(result);
    }
}
