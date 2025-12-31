using System.Security.Cryptography;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Bll.Abstractions.ForService;
using CheckMyStar.Bll.Requests;
using CheckMyStar.Bll.Models;

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
    /// <summary>
    /// Asynchronously validates a user's credentials based on the specified request.
    /// </summary>
    /// <param name="request">An object containing the user's identification and credential information to be validated. Cannot be null.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the validated user if the
    /// credentials are correct; otherwise, null.</returns>
    public async Task<UserModel?> ValidateUserAsync(LoginGetRequest request)
    {
        string password = request.Password;

        request.Password = HashPassword(request.Password);

        var user = await userBusForService.GetUser(request);

        if (user != null)
        {
            var isValid = VerifyPassword(password, user.Password);

            return isValid ? user : null;
        }

        return user;
    }

    /// <summary>
    /// Computes a SHA-256 hash of the specified password and returns the result as a hexadecimal string.
    /// </summary>
    /// <remarks>The returned hash is suitable for storage or comparison, but this method does not apply
    /// salting or key stretching. For secure password storage, consider using a dedicated password hashing algorithm
    /// such as PBKDF2, bcrypt, or Argon2.</remarks>
    /// <param name="password">The password to hash. Cannot be null.</param>
    /// <returns>A hexadecimal string representation of the SHA-256 hash of the password.</returns>
    public string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashBytes);
        }
    }

    /// <summary>
    /// Verifies whether the specified password matches the given password hash.
    /// </summary>
    /// <remarks>This method uses the same hashing algorithm as the one used to generate the provided password
    /// hash. Ensure that the hash was created using a compatible method to avoid false negatives.</remarks>
    /// <param name="password">The plain text password to verify. Cannot be null.</param>
    /// <param name="passwordHash">The hashed password to compare against. Cannot be null.</param>
    /// <returns>true if the password matches the hash; otherwise, false.</returns>
    public bool VerifyPassword(string password, string passwordHash)
    {
        string computedHash = HashPassword(password);

        return computedHash == passwordHash;
    }
}