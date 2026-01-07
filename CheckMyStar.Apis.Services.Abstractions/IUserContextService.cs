using CheckMyStar.Apis.Services.Models;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public class IUserContextService
    {
        UserContext CurrentUser { get; } = null!;
    }
}
