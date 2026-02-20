using CheckMyStar.Bll.Responses;

namespace CheckMyStar.Bll.Abstractions
{
    public interface IActivityBus
    {
        Task<ActivitiesResponse> GetActivities(int numberDays, CancellationToken ct);
        Task<ActivityResponse> AddActivity(string description, DateTime date, int user, bool isSuccess, CancellationToken ct);
        Task<BaseResponse> DeleteUserActivities(int userIdentifier, CancellationToken ct);
    }
}
