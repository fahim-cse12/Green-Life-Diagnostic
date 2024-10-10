using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PatientInvestigation : BaseEntity
    {
        [Column("PatientInvestigationId")]
        public Guid PatientInvestigationId { get; set; }
        public string? PatientUniqueId { get; set; }
        public Guid? DoctorId { get; set; } // Nullable for patients who don’t visit a doctor
        public string PatientInvestigationUniqueId { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientMobileNo { get; set; }
        public string PatientAddress { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime? DeliveryDate { get; set; } // Nullable
        public bool IsDelivered { get; set; }

        public ICollection<PatientInvestigationDetail> InvestigationDetails { get; set; } // Relation to details
    }

    //public class PatientInvestigation : BaseEntity
    //{
    //    [Column("PatientInvestigationId")]
    //    public Guid Id { get; set; }
    //    public string? PatientUniqueId { get; set; }
    //    [ForeignKey(nameof(Doctor))]
    //    public Guid? DoctorId { get; set; }
    //    [ForeignKey(nameof(Investigation))]
    //    public Guid InvestigationId { get; set; }
    //    public string InvestigationUniqueId { get; set; }
    //    public string PatientName { get; set; } 
    //    public int PatientAge { get; set; }
    //    public string PatientMobileNo { get; set; }
    //    public string PatientAddress { get; set; } 
    //    public decimal DiscountAmount { get; set; }
    //    public decimal DueAmount { get; set; }
    //    public decimal PaidAmount { get; set; }
    //    public DateTime DeliveryDate { get; set; }
    //    public bool IsDelivered { get; set; }
    //}
}
