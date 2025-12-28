using CheckMyStar.Apis.Models;

namespace CheckMyStar.Apis.Services;

/// <summary>
/// Defines methods for authenticating users and managing password security within an application.
/// </summary>
/// <remarks>Implementations of this interface provide user validation and password hashing functionality. Methods
/// are designed to support secure authentication workflows, including verifying user credentials and storing passwords
/// safely. Thread safety and specific hashing algorithms depend on the concrete implementation.</remarks>
public interface IAuthService
{
    /// <summary>
    /// Asynchronously validates the specified user's credentials and returns the corresponding user if authentication
    /// succeeds.
    /// </summary>
    /// <param name="username">The username to authenticate. Cannot be null or empty.</param>
    /// <param name="password">The password associated with the specified username. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the authenticated <see cref="User"/>
    /// if the credentials are valid; otherwise, <see langword="null"/>.</returns>
    Task<User?> ValidateUserAsync(string username, string password);
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