using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IActivityDal
    {
        Task<ActivityResult> GetNextIdentifier(CancellationToken ct);
        Task<ActivitiesResult> GetActivities(int numberDays, CancellationToken ct);
        Task<ActivitiesResult> GetActivities(string? lastName, string? firstName, string? description, DateTime? date, bool? isSuccess, CancellationToken ct);
        Task<BaseResult> AddActivity(Activity activity, CancellationToken ct);
        Task<BaseResult> DeleteUserActivities(int userIdentifier, CancellationToken ct);
    }
}
