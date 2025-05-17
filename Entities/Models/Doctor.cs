using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Doctor : BaseEntity
    {
        [Column("DoctorId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal FeeForNewPatient { get; set; }
        public decimal FeeForOldPatient { get; set; }
        public string Speciality { get; set; }
        public string ScheduledDay { get; set; }
        public string ContactNo { get; set; }   
    }
}
