using System.Security.Cryptography;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Security;

namespace CheckMyStar.Apis.Services;

/// <summary>
/// Provides authentication services for validating user credentials and managing password security within the
/// application.
/// </summary>
/// <remarks>This class offers methods for verifying user identities and securely handling passwords using salted
/// hashing. It is intended for use as an authentication provider and should be integrated with a persistent user store
/// in production environments. The implementation is not thread-safe and is designed for demonstration or testing
/// purposes only.</remarks>
public class AuthenticateService(IUserBusForService userBusForService) : IAuthenticateService
{
    private static readonly Dictionary<string, UserResponse> RefreshTokens = new();

    /// <summary>
    /// Asynchronously validates a user's credentials based on the specified request.
    /// </summary>
    /// <param name="request">An object containing the user's identification and credential information to be validated. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the validated user if the
    /// credentials are correct; otherwise, null.</returns>
    public async Task<UserResponse> ValidateUserAsync(LoginGetRequest request, CancellationToken ct)
    {
        string password = request.Password;

        request.Password = SecurityHelper.HashPassword(request.Password);

        var user = await userBusForService.GetUser(request, ct);

        if (user.IsSuccess && user.User != null)
        {
            user.IsValid = SecurityHelper.VerifyPassword(password, user.User.Password);
        }

        return user;
    }

    /// <summary>
    /// Asynchronously validates a user's change password credentials based on the specified request.
    /// </summary>
    /// <param name="request">An object containing the user's identification and credential information to be validated. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the validated user if the
    /// credentials are correct; otherwise, null.</returns>
    public async Task<UserResponse> ValidateUserAsync(PasswordGetRequest request, CancellationToken ct)
    {
        string oldPassword = request.OldPassword;        

        var user = await userBusForService.GetUser(request, ct);

        if (user.IsSuccess && user.User != null)
        {
            user.IsValid = true;
        }

        return user;
    }

    public Task<UserResponse?> ValidateRefreshTokenAsync(string refreshToken, CancellationToken ct)
    {
        if (RefreshTokens.TryGetValue(refreshToken, out var user))
        {
            return Task.FromResult<UserResponse?>(user);
        }

        return Task.FromResult<UserResponse?>(null);
    }

    public void StoreRefreshToken(string refreshToken, UserResponse user)
    {
        RefreshTokens[refreshToken] = user;
    }

    public void RemoveRefreshToken(string refreshToken)
    {
        RefreshTokens.Remove(refreshToken);
    }
}