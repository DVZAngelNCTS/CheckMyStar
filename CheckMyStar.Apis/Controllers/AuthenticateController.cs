using CheckMyStar.Apis.Services;
using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Enumerations;
using CheckMyStar.Security;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
public class AuthenticateController(ILogger<AuthenticateController> logger, IConfiguration configuration, IAuthenticateService authenticateService, IUserService userService) : ControllerBase
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
            logger.LogError("Utilisateur ou mot de passe incorrect: {Login}", request.Login);

            return Ok(new LoginResponse
            {
                IsSuccess = false,
                IsValid = false,
                Message = $"Utilisateur ou mot de passe incorrect",
                Login = new LoginModel { 
                    Token = string.Empty, 
                    User = null
                }
            });
        }

        if (user.IsValid != true || user.User == null)
        {
            logger.LogWarning("Utilisateur ou mot de passe incorrect");

            return Unauthorized(new { message = "Utilisateur ou mot de passe incorrect" });
        }

        var token = GenerateJwtToken(user.User);
        var refreshToken = GenerateSecureRefreshToken();

        authenticateService.StoreRefreshToken(refreshToken, user);

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
                RefreshToken = refreshToken,
                User = user.User
            }
        });
    }

    /// <summary>
    /// Authenticates a user based on the provided credentials and returns a JWT token if authentication is successful.
    /// </summary>
    /// <param name="request">The user credentials to authenticate. Must include a valid username and password.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>An HTTP 200 response containing a JWT token and user information if authentication succeeds; otherwise, an HTTP
    /// 401 response indicating invalid credentials.</returns>
    [HttpPost("updatepassword")]
    public async Task<IActionResult> UpdatePassword([FromBody] PasswordGetRequest request, CancellationToken ct)
    {
        var user = await authenticateService.ValidateUserAsync(request, ct);

        if (!user.IsSuccess)
        {
            logger.LogError("Erreur lors de la tentative de changement de mot de passe, impossible de valider l'utilisateur");

            return Ok(new PasswordResponse
            {
                IsSuccess = false,
                IsValid = false,
                Message = $"Erreur lors de la tentative de changement de mot de passe, impossible de valider l'utilisateur",
                Login = new LoginModel { Token = string.Empty, User = null }
            });
        }

        if (user.IsValid != true || user.User == null)
        {
            logger.LogWarning("Échec de connexion pour l'utilisateur, impossible de valider l'utilisateur");

            return Unauthorized(new { message = "Nom d'utilisateur ou mot de passe incorrect" });
        }

        user.User.Password = SecurityHelper.HashPassword(request.NewPassword); 
        user.User.IsFirstConnection = false;

        UserSaveRequest updateSaveRequest = new UserSaveRequest
        {
            User = user.User
        };

        var result = await userService.UpdateUser(updateSaveRequest, ct);

        if (result.IsSuccess)
        {
            var token = GenerateJwtToken(user.User);
            var refreshToken = GenerateSecureRefreshToken();

            authenticateService.StoreRefreshToken(refreshToken, user);

            var message = $"Connexion réussie pour l'utilisateur: {user.User.LastName} {user.User.FirstName}";

            logger.LogInformation("Connexion réussie pour l'utilisateur: {LastName} {FirstName}", user.User.LastName, user.User.FirstName);

            return Ok(new PasswordResponse
            {
                IsSuccess = true,
                IsValid = true,
                Message = message,
                Login = new LoginModel
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    User = user.User
                }
            });
        }
        else
        {
            logger.LogError("Erreur lors de la tentative de changement de mot de passe pour l'utilisateur: {Login}", request.Login);

            return Ok(new PasswordResponse
            {
                IsSuccess = false,
                IsValid = false,
                Message = $"Erreur lors de la tentative de changement de mot de passe pour l'utilisateur: {request.Login}",
                Login = new LoginModel 
                { 
                    Token = string.Empty, 
                    User = null 
                }
            });
        }
    }

    /// <summary>
    /// Refresh token
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request, CancellationToken ct)
    {
        var user = await authenticateService.ValidateRefreshTokenAsync(request.RefreshToken, ct);

        if (user == null)
            return Unauthorized(new { message = "Invalid refresh token" });

        var newAccessToken = GenerateJwtToken(user.User!);
        var newRefreshToken = GenerateSecureRefreshToken();

        // Supprimer l'ancien
        authenticateService.RemoveRefreshToken(request.RefreshToken); 
        
        // Stocker le nouveau
        authenticateService.StoreRefreshToken(newRefreshToken, user);

        var message = $"Connexion rafraichie pour l'utilisateur: {user.User!.LastName} {user.User.FirstName}";

        logger.LogInformation("Connexion rafraichie pour l'utilisateur: {LastName} {FirstName}", user.User.LastName, user.User.FirstName);

        return Ok(new LoginResponse
        {
            IsSuccess = true,
            IsValid = true,
            Message = message,
            Login = new LoginModel
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
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

    private string GenerateSecureRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}