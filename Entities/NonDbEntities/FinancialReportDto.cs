namespace Entities.NonDbEntities;
public class FinancialReportDto : INonEntityBase
{
    public DateTime RecordDate { get; set; }
    public string Purpose { get; set; }
    public int FinancialType { get; set; }
    public decimal Income { get; set; }
    public decimal Expense { get; set; }
    public decimal Asset { get; set; }
    public decimal Liability { get; set; }
    public Guid? DoctorId { get; set; }
    public string PatientName { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal DueAmount { get; set; }
    public decimal PaidAmount { get; set; }
}
