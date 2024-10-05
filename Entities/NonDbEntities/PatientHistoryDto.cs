using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.NonDbEntities
{
    public class PatientHistoryDto : INonEntityBase
    {
        public Guid TicketId { get; set; }
        public Guid PatientId { get; set; }
        public string TicketNo { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public int PatientType { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public DateTime LastVisited { get; set; }
        public string DoctorName { get; set; }
    }
}
