using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using CheckMyStar.Apis.Services;
using CheckMyStar.Apis.Models;

namespace CheckMyStar.Apis.Controllers;

/// <summary>
/// Defines API endpoints for user authentication operations, such as logging in and obtaining a JSON Web Token (JWT).
/// </summary>
/// <remarks>This controller provides authentication functionality for clients by validating user credentials and
/// issuing JWTs for authorized access to protected resources. The controller is configured with API routing and is
/// intended to be used as part of an ASP.NET Core Web API. All endpoints require appropriate request payloads and may
/// return standard HTTP status codes such as 200 (OK) or 401 (Unauthorized) based on authentication results.</remarks>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Initializes a new instance of the AuthController class using the specified configuration settings.
    /// </summary>
    /// <param name="configuration">The application configuration settings used to initialize the controller. Cannot be null.</param>
    /// <param name="authService">The authentication service used to validate user credentials. Cannot be null.</param>
    /// <param name="logger">The logger instance for logging authentication events. Cannot be null.</param>
    public AuthController(
        IConfiguration configuration,
        IAuthService authService,
        ILogger<AuthController> logger)
    {
        _configuration = configuration;
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Authenticates a user based on the provided credentials and returns a JWT token if authentication is successful.
    /// </summary>
    /// <remarks>This endpoint is typically used to obtain a JWT token for subsequent authenticated requests.
    /// The client should include the returned token in the Authorization header of future requests.</remarks>
    /// <param name="request">The login request containing the user's credentials. Must not be null.</param>
    /// <returns>An <see cref="OkObjectResult"/> containing a JWT token if authentication succeeds; otherwise, an <see
    /// cref="UnauthorizedResult"/> if the credentials are invalid.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _authService.ValidateUserAsync(request.Username, request.Password);
        
        if (user is null)
        {
            _logger.LogWarning("Échec de connexion pour l'utilisateur: {Username}", request.Username);
            return Unauthorized(new { message = "Nom d'utilisateur ou mot de passe incorrect" });
        }

        var token = GenerateJwtToken(user);
        _logger.LogInformation("Connexion réussie pour l'utilisateur: {Username}", user.Username);
        
        return Ok(new
        {
            token,
            username = user.Username,
            email = user.Email,
            roles = user.Roles
        });
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        // Ajouter les rôles comme claims
        foreach (var role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

/// <summary>
/// Represents a request to authenticate a user with a username and password.
/// </summary>
/// <param name="Username">The username of the user attempting to log in. Cannot be null or empty.</param>
/// <param name="Password">The password associated with the specified username. Cannot be null or empty.</param>
public record LoginRequest(string Username, string Password);