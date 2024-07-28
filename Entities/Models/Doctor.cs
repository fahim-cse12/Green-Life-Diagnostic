using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
