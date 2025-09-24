namespace Entities.NonDbEntities;
public class DashboardData :INonEntityBase
{
    public int TotalDoctors { get; set; }
    public int TotalPatients { get; set; }
    public int TotalInvestigations { get; set; }
    public int TotalTickets { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpense { get; set; }

    public int TodayDoctors { get; set; }
    public int TodayPatients { get; set; }
    public int TodayInvestigations { get; set; }
    public int TodayTickets { get; set; }
    public decimal? TodayIncome { get; set; }
    public decimal? TodayExpense { get; set; }
    public int TodayPendingTests { get; set; }
    public int TodayDeliveredTests { get; set; }
}