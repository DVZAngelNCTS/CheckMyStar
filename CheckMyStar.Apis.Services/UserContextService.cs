using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using CheckMyStar.Apis.Services.Abstractions;
using CheckMyStar.Apis.Services.Models;
using CheckMyStar.Enumerations;

namespace CheckMyStar.Apis.Services
{
    public class UserContextService : IUserContextService
    {
        public UserContext CurrentUser { get; } = null!;

        public UserContextService(IHttpContextAccessor accessor)
        {
            var user = accessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                var token = accessor.HttpContext?.Request.Headers["Authorization"]
                    .ToString()
                    .Replace("Bearer ", "");

                var identifierClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                var firstNameClaim = user.FindFirst(ClaimTypes.GivenName);
                var lastNameClaim = user.FindFirst(ClaimTypes.Surname);
                var roleClaim = user.FindFirst(ClaimTypes.Role);

                CurrentUser = new UserContext
                {
                    Token = token ?? "",
                    Identifier = identifierClaim != null ? Convert.ToInt32(identifierClaim.Value) : 0,
                    FirstName = firstNameClaim?.Value ?? "",
                    LastName = lastNameClaim?.Value ?? "",
                    Role = roleClaim != null ? roleClaim.Value.ToEnum<EnumRole>() : EnumRole.User
                };
            }

        }

    }
}
