namespace Entities.NonDbEntities;
public class AppointmentWiseIncomeDto : INonEntityBase
{
    public DateTime AppointmentDate { get; set; }
    public string DoctorName { get; set; }
    public string PatientName { get; set; }
    public int Age { get; set; }
    public bool IsNewPatient { get; set; }
    public decimal DoctorFee { get; set; }
    public decimal DiscountAmount { get; set; }
    public int TotalRecords { get; set; }
}
