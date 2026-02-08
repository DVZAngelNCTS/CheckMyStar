namespace CheckMyStar.Bll.Models
{
    public class DashboardModel
    {
        public int NumberUsers { get; set; }
        public int NumberUserActives { get; set; }
        public int NumberUserWeeklyTrend { get; set; }
        public int NumberUserMonthlyTrend { get; set; }
        public int NumberUserYearlyTrend { get; set; }
        public decimal PercentageUserActive { get; set; }
        public decimal PercentageUserWeeklyTrend { get; set; }
        public decimal PercentageUserMonthlyTrend { get; set; }
        public decimal PercentageUserYearlyTrend { get; set; }
        public int NumberRoles { get; set; }
    }
}
