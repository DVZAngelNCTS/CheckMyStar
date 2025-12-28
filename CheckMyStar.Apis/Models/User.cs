namespace CheckMyStar.Apis.Models;

/// <summary>
/// Represents an application user with authentication and authorization information.
/// </summary>
/// <remarks>The User class encapsulates identifying and credential data, including username, password hash, email
/// address, and assigned roles. It is typically used to manage user accounts, verify credentials, and determine access
/// permissions within the application.</remarks>
public class User
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the username associated with the user account.
    /// </summary>
    public required string Username { get; set; }
    /// <summary>
    /// Gets or sets the hashed representation of the user's password.
    /// </summary>
    public required string PasswordHash { get; set; }
    /// <summary>
    /// Gets or sets the email address associated with the user.
    /// </summary>
    public required string Email { get; set; }
    /// <summary>
    /// Gets or sets the list of roles assigned to the user.
    /// </summary>
    public List<string> Roles { get; set; } = [];
}