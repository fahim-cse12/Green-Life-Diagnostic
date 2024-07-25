using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Patient : BaseEntity
    {
        [Column("PatientId")]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public int PatientType { get; set; } // "Old" or "New"
        public ICollection<Ticket> Tickets { get; set; }
    }
}
