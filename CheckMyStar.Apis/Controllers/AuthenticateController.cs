using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Models;
using CheckMyStar.Enumerations;
using CheckMyStar.Bll.Responses;

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
public class AuthenticateController(ILogger<AuthenticateController> logger, IConfiguration configuration, IAuthenticateService authenticateService) : ControllerBase
{
    /// <summary>
    /// Authenticates a user based on the provided credentials and returns a JWT token if authentication is successful.
    /// </summary>
    /// <param name="request">The user credentials to authenticate. Must include a valid username and password.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>An HTTP 200 response containing a JWT token and user information if authentication succeeds; otherwise, an HTTP
    /// 401 response indicating invalid credentials.</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginGetRequest request, CancellationToken ct)
    {
        var user = await authenticateService.ValidateUserAsync(request, ct);

        if (!user.IsSuccess)
        {
            logger.LogError("Erreur lors de la tentative de connexion pour l'utilisateur: {Login}", request.Login);

            return Ok(new LoginResponse
            {
                IsSuccess = false,
                IsValid = false,
                Message = $"Erreur lors de la tentative de connexion pour l'utilisateur: {request.Login}",
                Login = new LoginModel { Token = string.Empty, User = null }
            });
        }

        if (user.IsValid != true || user.User == null)
        {
            logger.LogWarning("Échec de connexion pour l'utilisateur: {Login}", request.Login);

            return Unauthorized(new { message = "Nom d'utilisateur ou mot de passe incorrect" });
        }

        var token = GenerateJwtToken(user.User);

        var message = $"Connexion réussie pour l'utilisateur: {user.User.LastName} {user.User.FirstName}";
        
        logger.LogInformation("Connexion réussie pour l'utilisateur: {LastName} {FirstName}", user.User.LastName, user.User.FirstName);

        return Ok(new LoginResponse
        {
            IsSuccess = true,
            IsValid = true,
            Message = message,
            Login = new LoginModel
            {
                Token = token,
                User = user.User
            }
        });
    }

    private string GenerateJwtToken(UserModel user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Identifier.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Identifier.ToString()),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName)
        };

        // Ajouter les rôles comme claims
        claims.Add(new Claim(ClaimTypes.Role, user.Role.ToStringValue()));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                Convert.ToDouble(configuration["Jwt:ExpiryInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}