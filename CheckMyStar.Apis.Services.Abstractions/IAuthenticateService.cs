using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Apis.Services.Abstractions;

/// <summary>
/// Defines methods for authenticating users and managing password security within an application.
/// </summary>
/// <remarks>Implementations of this interface provide user validation and password hashing functionality. Methods
/// are designed to support secure authentication workflows, including verifying user credentials and storing passwords
/// safely. Thread safety and specific hashing algorithms depend on the concrete implementation.</remarks>
public interface IAuthenticateService
{
    /// <summary>
    /// Asynchronously validates the specified user request and retrieves the corresponding user if validation succeeds.
    /// </summary>
    /// <param name="request">The user request containing the criteria used to identify and validate the user. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the validated user if the request is
    /// valid and a matching user is found; otherwise, null.</returns>
    Task<UserResponse> ValidateUserAsync(LoginGetRequest request, CancellationToken ct);
    /// <summary>
    /// Asynchronously validates refresh token
    /// </summary>
    /// <param name="refreshToken">The refreshToken</param>
    /// <returns></returns>
    Task<UserResponse?> ValidateRefreshTokenAsync(string refreshToken, CancellationToken ct);
    /// <summary>
    /// Store refresh token
    /// </summary>
    /// <param name="refreshToken">The refreshToken</param>
    void StoreRefreshToken(string refreshToken, UserResponse user);
    /// <summary>
    /// Remove refresh token
    /// </summary>
    /// <param name="refreshToken">The refreshToken</param>
    void RemoveRefreshToken(string refreshToken);
}