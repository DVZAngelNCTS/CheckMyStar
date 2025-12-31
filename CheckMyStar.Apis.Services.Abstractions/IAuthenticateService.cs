using CheckMyStar.Bll.Models;
using CheckMyStar.Bll.Requests;

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
    Task<UserModel?> ValidateUserAsync(LoginGetRequest request);
    /// <summary>
    /// Generates a secure hash for the specified password.
    /// </summary>
    /// <remarks>The returned hash can be stored and used for password verification. The method does not
    /// perform password validation; callers should ensure the password meets any required complexity or length
    /// constraints before calling this method.</remarks>
    /// <param name="password">The plain text password to be hashed. Cannot be null or empty.</param>
    /// <returns>A string containing the hashed representation of the password.</returns>
    string HashPassword(string password);
    /// <summary>
    /// Determines whether the specified password matches the provided password hash.
    /// </summary>
    /// <param name="password">The plain-text password to verify against the hash. Cannot be null.</param>
    /// <param name="passwordHash">The hashed password to compare with. Cannot be null.</param>
    /// <returns>true if the password matches the hash; otherwise, false.</returns>
    bool VerifyPassword(string password, string passwordHash);
}