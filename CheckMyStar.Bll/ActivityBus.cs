using AutoMapper;

using CheckMyStar.Bll.Abstractions;
using CheckMyStar.Bll.Responses;
using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Data;

namespace CheckMyStar.Bll
{
    public partial class ActivityBus(IActivityDal activityDal, IMapper mapper) : IActivityBus
    {
        public async Task<ActivitiesResponse> GetActivities(int numberDays, CancellationToken ct)
        {
            var activities = await activityDal.GetActivities(numberDays, ct);

            return mapper.Map<ActivitiesResponse>(activities);
        }

        public async Task<ActivitiesResponse> GetActivities(string? lastName, string? firstName, string? description, DateTime? date, bool? isSuccess, CancellationToken ct)
        {
            var activities = await activityDal.GetActivities(lastName, firstName, description, date, isSuccess, ct);

            return mapper.Map<ActivitiesResponse>(activities);
        }

        public async Task<ActivityResponse> AddActivity(string description, DateTime date, int user, bool isSuccess, CancellationToken ct)
        {
            ActivityResponse activityResponse = new ActivityResponse();

            var result = await activityDal.GetNextIdentifier(ct);

            if (result.IsSuccess)
            {
                if (result.Activity != null)
                {
                    Activity activityEntity = new Activity()
                    {
                        Identifier = result.Activity.Identifier,
                        Description = description,
                        Date = date,
                        User = user,
                        IsSuccess = isSuccess
                    };

                    var baseResult = await activityDal.AddActivity(activityEntity, ct);

                    if (baseResult.IsSuccess)
                    {
                        activityResponse.IsSuccess = true;
                        activityResponse.Message = "Activité ajouté avec succès";
                    }
                    else
                    {
                        activityResponse.IsSuccess = false;
                        activityResponse.Message = baseResult.Message;
                    }
                }
                else
                {
                    activityResponse.IsSuccess = false;
                    activityResponse.Message = "Impossible d'ajouter l'activité";
                }
            }
            else
            {
                activityResponse.IsSuccess = false;
                activityResponse.Message = result.Message;
            }

            return activityResponse;
        }

        public async Task<BaseResponse> DeleteUserActivities(int userIdentifier, CancellationToken ct)
        {
            ActivityResponse activityResponse = new ActivityResponse();

            var result = await activityDal.DeleteUserActivities(userIdentifier, ct);

            if (result.IsSuccess)
            {
                activityResponse.IsSuccess = true;
                activityResponse.Message = "Activités supprimé avec succès";
            }
            else
            {
                activityResponse.IsSuccess = false;
                activityResponse.Message = result.Message;
            }

            return activityResponse;
        }
    }
}
