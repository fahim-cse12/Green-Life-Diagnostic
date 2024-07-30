using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Ticket : BaseEntity
    {
        [Column("TicketId")]
        public Guid Id { get; set; }
        [ForeignKey(nameof(Patient))]
        public Guid PatientId { get; set; }
        [ForeignKey(nameof(Doctor))]
        public Guid DoctorId { get; set; }
        public string UniqueId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
         
    }

}
