using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Patient : BaseEntity
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Age { get; set; }
        public string Address { get; set; }
        public string PatientType { get; set; } // "Old" or "New"
        public ICollection<Ticket> Tickets { get; set; }
    }
}
