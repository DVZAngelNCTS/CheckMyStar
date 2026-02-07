using CheckMyStar.Apis.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheckMyStar.Apis.Controllers
{
    /// <summary>
    /// Represents an API controller that provides endpoints for managing and retrieving country data.
    /// </summary>
    /// <remarks>This controller is intended for use in ASP.NET Core applications and requires users to have
    /// the 'Administrator' role to access its endpoints. All routes are prefixed with 'api/country'.</remarks>
    /// <param name="countryService">The service used to access and manage country information.</param>
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
