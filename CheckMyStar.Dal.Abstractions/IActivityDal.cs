using CheckMyStar.Dal.Results;
using CheckMyStar.Data;

namespace CheckMyStar.Dal.Abstractions
{
    public interface IActivityDal
    {
        Task<ActivityResult> GetNextIdentifier(CancellationToken ct);
        Task<ActivitiesResult> GetActivities(int numberDays, CancellationToken ct);
        Task<BaseResult> AddActivity(Activity activity, CancellationToken ct);
    }
}
