using System.Security.Cryptography;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using CheckMyStar.Apis.Models;

namespace CheckMyStar.Apis.Services;

/// <summary>
/// Provides authentication services for validating user credentials and managing password security within the
/// application.
/// </summary>
/// <remarks>This class offers methods for verifying user identities and securely handling passwords using salted
/// hashing. It is intended for use as an authentication provider and should be integrated with a persistent user store
/// in production environments. The implementation is not thread-safe and is designed for demonstration or testing
/// purposes only.</remarks>
public class AuthService : IAuthService
{
    // En production, utilisez Entity Framework ou un autre ORM
    private readonly List<User> _users =
    [
        new User
        {
            Id = 1,
            Username = "admin",
            Email = "admin@checkmystar.com",
            PasswordHash = "", // Sera initialisé dans le constructeur
            Roles = ["Admin", "User"]
        },
        new User
        {
            Id = 2,
            Username = "user1",
            Email = "user1@checkmystar.com",
            PasswordHash = "", // Sera initialisé dans le constructeur
            Roles = ["User"]
        }
    ];

    /// <summary>
    /// Initializes a new instance of the AuthService class and sets up default user password hashes.
    /// </summary>
    /// <remarks>This constructor pre-populates the password hashes for the default users. The initial
    /// credentials are intended for demonstration or testing purposes and should be changed in a production
    /// environment.</remarks>
    public AuthService()
    {
        // Initialiser les mots de passe hashés
        _users[0].PasswordHash = HashPassword("admin123");
        _users[1].PasswordHash = HashPassword("user123");
    }

    /// <summary>
    /// Asynchronously validates a user's credentials and returns the corresponding user if authentication succeeds.
    /// </summary>
    /// <param name="username">The username of the user to validate. Cannot be null or empty.</param>
    /// <param name="password">The password to verify for the specified user. Cannot be null or empty.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the authenticated <see cref="User"/>
    /// if the credentials are valid; otherwise, <see langword="null"/>.</returns>
    public Task<User?> ValidateUserAsync(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username);
        
        if (user is null)
        {
            return Task.FromResult<User?>(null);
        }

        var isValid = VerifyPassword(password, user.PasswordHash);
        return Task.FromResult(isValid ? user : null);
    }

    /// <summary>
    /// Generates a hashed representation of the specified password using a randomly generated salt and PBKDF2 with
    /// HMACSHA256.
    /// </summary>
    /// <remarks>The returned string can be stored for later password verification. Each call generates a
    /// unique salt, resulting in a different hash for the same password. This method does not perform password
    /// validation or enforce password complexity requirements.</remarks>
    /// <param name="password">The plain-text password to be hashed. Cannot be null.</param>
    /// <returns>A string containing the base64-encoded salt and hashed password, separated by a period.</returns>
    public string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return $"{Convert.ToBase64String(salt)}.{hashed}";
    }

    /// <summary>
    /// Verifies whether the specified password matches the provided password hash.
    /// </summary>
    /// <remarks>The password hash must be in the format "{salt}.{hash}", where both components are
    /// base64-encoded. If the format is invalid, the method returns false. This method uses PBKDF2 with HMACSHA256 for
    /// password verification.</remarks>
    /// <param name="password">The plaintext password to verify against the stored hash.</param>
    /// <param name="passwordHash">The hashed password value, consisting of a base64-encoded salt and hash separated by a period.</param>
    /// <returns>true if the password matches the hash; otherwise, false.</returns>
    public bool VerifyPassword(string password, string passwordHash)
    {
        var parts = passwordHash.Split('.');
        if (parts.Length != 2)
        {
            return false;
        }

        var salt = Convert.FromBase64String(parts[0]);
        var hash = parts[1];

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

        return hash == hashed;
    }
}