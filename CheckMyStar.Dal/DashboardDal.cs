using Microsoft.EntityFrameworkCore;

using CheckMyStar.Dal.Abstractions;
using CheckMyStar.Dal.Models;
using CheckMyStar.Dal.Results;
using CheckMyStar.Data.Abstractions;

namespace CheckMyStar.Dal
{
    public class DashboardDal(ICheckMyStarDbContext dbContext) : IDashboardDal
    {
        public async Task<DashboardResult> GetDashboard(CancellationToken ct)
        {
            DashboardResult dashboardResult = new DashboardResult();

            try
            {
                var today = DateTime.Today;

                var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);

                var endOfWeek = startOfWeek.AddDays(7);

                var numberUsers = await (from u in dbContext.Users.AsNoTracking()
                                         select u).CountAsync();

                var numberUserActives = await (from u in dbContext.Users.AsNoTracking()
                                               where
                                                u.IsActive == true
                                               select u).CountAsync();

                var numberUserWeeklyTrend = await (from u in dbContext.Users.AsNoTracking()
                                                   where
                                                        u.CreatedDate != null
                                                     && u.CreatedDate.Value >= startOfWeek
                                                     && u.CreatedDate.Value <= endOfWeek
                                                    select u).CountAsync();

                var numberUserMonthlyTrend = await (from u in dbContext.Users.AsNoTracking()
                                                    where
                                                        u.CreatedDate != null
                                                     && u.CreatedDate.Value.Month == DateTime.Now.Month
                                                    select u).CountAsync();

                var numberUserYearlyTrend = await (from u in dbContext.Users.AsNoTracking()
                                                   where
                                                        u.CreatedDate != null
                                                     && u.CreatedDate.Value.Year == DateTime.Now.Year
                                                   select u).CountAsync();

                var numberRoles = await (from r in dbContext.Roles.AsNoTracking()
                                         select r).CountAsync();

                dashboardResult.IsSuccess = true;
                dashboardResult.Dashboard = new Dashboard()
                {
                    NumberUsers = numberUsers,
                    NumberUserActives = numberUserActives,
                    PercentageUserActive = (numberUserActives / numberUsers) * 100.0m,
                    NumberUserWeeklyTrend = numberUserWeeklyTrend,
                    PercentageUserWeeklyTrend = (numberUserWeeklyTrend / numberUsers) * 100.0m,
                    NumberUserMonthlyTrend = numberUserMonthlyTrend,
                    PercentageUserMonthlyTrend = (numberUserMonthlyTrend / numberUsers) * 100.0m,
                    NumberUserYearlyTrend = numberUserYearlyTrend,
                    PercentageUserYearlyTrend = (numberUserYearlyTrend / numberUsers) * 100.0m,
                    NumberRoles = numberRoles,
                };
            }
            catch (Exception ex)
            {
                dashboardResult.IsSuccess = false;
                dashboardResult.Message = ex.Message;
            }

            return dashboardResult;
        }
    }
}
