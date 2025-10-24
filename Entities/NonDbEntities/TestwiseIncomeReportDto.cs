namespace Entities.NonDbEntities;
public class TestWiseIncomeReportDto : INonEntityBase
{
    public Guid PatientInvestigationId { get; set; }
    public DateOnly TestDate { get; set; }
    public string? Id { get; set; }
    public string PatientName { get; set; }
    public int PatientAge { get; set; }
    public string? DoctorName { get; set; }
    public string InvestigationName { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal PaidAmount { get; set; }
    public decimal DueAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public bool IsDelivered { get; set; }
    public int TotalRecords { get; set; }
}
