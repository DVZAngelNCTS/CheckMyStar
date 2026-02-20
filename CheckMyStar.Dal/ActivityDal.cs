using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data.Abstractions;
using CheckMyStar.Data;
using CheckMyStar.Dal.Models;

namespace CheckMyStar.Dal
{
    public class ActivityDal(ICheckMyStarDbContext dbContext) : IActivityDal
    {
        public async Task<ActivityResult> GetNextIdentifier(CancellationToken ct)
        {
            ActivityResult activityResult = new ActivityResult();

            try
            {
                var existingIdentifiers = await (from r in dbContext.Activities.AsNoTracking()
                                                 orderby r.Identifier
                                                 select r.Identifier).ToListAsync(ct);

                if (existingIdentifiers.Count == 0)
                {
                    activityResult.IsSuccess = true;
                    activityResult.Activity = new Activity { Identifier = 1 };
                    activityResult.Message = "Identifiant récupéré avec succès";
                }
                else
                {
                    int nextIdentifier = 1;

                    foreach (var id in existingIdentifiers)
                    {
                        if (id == nextIdentifier)
                        {
                            nextIdentifier++;
                        }
                        else if (id > nextIdentifier)
                        {
                            break;
                        }
                    }

                    activityResult.IsSuccess = true;
                    activityResult.Activity = new Activity { Identifier = nextIdentifier };
                    activityResult.Message = "Identifiant récupéré avec succès";
                }
            }
            catch (Exception ex)
            {
                activityResult.IsSuccess = false;
                activityResult.Message = ex.Message;
            }

            return activityResult;
        }

        public async Task<ActivitiesResult> GetActivities(int numberDays, CancellationToken ct)
        {
            ActivitiesResult activityResult = new ActivitiesResult();

            try
            {
                var date = DateTime.Now.AddDays(-numberDays);

                var activities = await (from a in dbContext.Activities.AsNoTracking()
                                        join u in dbContext.Users.AsNoTracking() on a.User equals u.Identifier
                                        where
                                            a.Date >= date
                                        select new UserActivity()
                                        {
                                            Date = a.Date,
                                            Description = a.Description,
                                            Identifier = a.Identifier,
                                            IsSuccess = a.IsSuccess,
                                            User = u
                                        }).ToListAsync();

                activityResult.IsSuccess = true;
                activityResult.Activities = activities;
            }
            catch (Exception ex)
            {
                activityResult.IsSuccess = false;
                activityResult.Message = ex.Message;
            }

            return activityResult;
        }

        public async Task<BaseResult> AddActivity(Activity activity, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                await dbContext.AddAsync(activity, ct);

                bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                if (result)
                {
                    baseResult.IsSuccess = true;
                    baseResult.Message = "Activité ajouté avec succès";
                }
                else
                {
                    baseResult.IsSuccess = false;
                    baseResult.Message = "Impossible d'ajouter l'activité";
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = "Impossible d'ajouter l'activité : " + ex.Message;
            }

            return baseResult;
        }

        public async Task<BaseResult> DeleteUserActivities(int userIdentifier, CancellationToken ct)
        {
            BaseResult baseResult = new BaseResult();

            try
            {
                var activities = await (from a in dbContext.Activities
                                        where a.User == userIdentifier
                                        select a).ToListAsync(ct);

                if (activities != null && activities.Count > 0)
                {
                    foreach (var activity in activities)
                    {
                        await dbContext.RemoveAsync(activity);
                    }

                    bool result = await dbContext.SaveChangesAsync() > 0 ? true : false;

                    if (result)
                    {
                        baseResult.IsSuccess = true;
                        baseResult.Message = "Activités supprimées avec succès";
                    }
                    else
                    {
                        baseResult.IsSuccess = false;
                        baseResult.Message = "Impossible de supprimer les activités";
                    }
                }
                else
                {
                    baseResult.IsSuccess = true;
                }
            }
            catch (Exception ex)
            {
                baseResult.IsSuccess = false;
                baseResult.Message = "Impossible de supprimer les activités : " + ex.Message;
            }

            return baseResult;
        }
    }
}
