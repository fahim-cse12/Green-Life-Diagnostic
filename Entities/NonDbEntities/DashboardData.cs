namespace Entities.NonDbEntities;
public class DashboardData :INonEntityBase
{
    // Total counts
    public int TotalDoctors { get; set; }
    public int TotalPatients { get; set; }
    public int TotalInvestigations { get; set; }
    public int TotalTickets { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpense { get; set; }

    // Today's counts
    public int TodayDoctors { get; set; }
    public int TodayPatients { get; set; }
    public int TodayInvestigations { get; set; }
    public int TodayTickets { get; set; }
    public decimal? TodayIncome { get; set; }
    public decimal? TodayExpense { get; set; }
    public int TodayPendingTests { get; set; }
    public int TodayDeliveredTests { get; set; }

    // Last month stats
    public int LastMonthPendingTests { get; set; }
    public int LastMonthDeliveredTests { get; set; }
    public decimal? LastMonthIncome { get; set; }
    public decimal? LastMonthExpense { get; set; }
    public decimal? LastMonthAsset { get; set; }
    public decimal? LastMonthLiability { get; set; }
    public int LastMonthInvestigations { get; set; }
    public int LastMonthTickets { get; set; }

    // Last year stats
    public decimal? LastYearIncome { get; set; }
    public decimal? LastYearExpense { get; set; }
}