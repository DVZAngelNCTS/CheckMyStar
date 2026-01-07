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
                var token = accessor.HttpContext?.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var identifier = user.FindFirst(ClaimTypes.NameIdentifier);
                var firstName = user.FindFirst(ClaimTypes.GivenName);
                var lastName = user.FindFirst(ClaimTypes.Surname);
                var role = user.FindFirst(ClaimTypes.Role);

                CurrentUser = new UserContext
                {
                    Token = token != null ? token : "",
                    Identifier = identifier != null ? int.Parse(identifier.Value) : 0,
                    FirstName = firstName != null ? firstName.Value : "",
                    LastName = lastName != null ? lastName.Value : "",
                    Role = role != null ? role.Value.ToEnum<EnumRole>() : EnumRole.User
                };
            }
        }

    }
}
