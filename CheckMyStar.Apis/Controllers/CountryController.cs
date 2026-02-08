using CheckMyStar.Apis.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Represents an API controller that provides endpoints for managing and retrieving country data.
    /// </summary>
    /// <param name="countryService">Country service</param>
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController(ICountryService countryService) : ControllerBase
    {
        /// <summary>
        /// Get countries
        /// </summary>
        /// <param name="ct">The cancellation token used to cancel the operation.</param>
        /// <returns>An IActionResult containing a collection of countries.</returns>
        [HttpPost("getcountries")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetCountries(CancellationToken ct)
        {
            var countries = await countryService.GetCountries(ct);

            return Ok(countries);
        }
    }
}
