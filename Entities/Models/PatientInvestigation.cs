using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class PatientInvestigation : BaseEntity
    {
        [Column("PatientInvestigationId")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Patient))]
        public Guid PatientId { get; set; }
        [ForeignKey(nameof(Doctor))]
        public Guid DoctorId { get; set; }
        [ForeignKey(nameof(Investigation))]
        public Guid InvestigationId { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PayAmount { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool IsDelivered { get; set; }
    }
}
