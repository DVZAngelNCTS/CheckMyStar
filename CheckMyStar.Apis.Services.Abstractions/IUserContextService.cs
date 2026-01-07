using CheckMyStar.Apis.Services.Models;

namespace CheckMyStar.Apis.Services.Abstractions
{
    public interface IUserContextService
    {
        UserContext CurrentUser { get; }
    }
}
