using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IUserDal
    {
        Task<UserResult> GetNextIdentifier(CancellationToken ct);
        Task<UserResult> GetUser(string login, string password, CancellationToken ct);
        Task<UserResult> GetUser(int identifier, CancellationToken ct);
        Task<UserResult> GetUser(string? lastName, string? firstName, int? SocietyIdentifier, string? email, string? phone, CancellationToken ct);
        Task<UsersResult> GetUsers(string? lastName, string? firstName, int? SocietyIdentifier, string? email, string? phone, string? address, int? role, CancellationToken ct);
        Task<BaseResult> AddUser(User user, CancellationToken ct);
        Task<BaseResult> UpdateUser(User user, CancellationToken ct);
        Task<BaseResult> DeleteUser(User user, CancellationToken ct);
        Task<UserEvolutionResult> GetUserEvolutions(CancellationToken ct);
    }
}
